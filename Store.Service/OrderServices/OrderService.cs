using AutoMapper;
using Store.Data.Entities;
using Store.Data.Entities.OrderEntities;
using Store.Repository.Interfaces;
using Store.Repository.Specification.OrderSpecs;
using Store.Service.OrderServices.OrderDtos;
using Store.Service.Services.BasketService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly IBasketService _basketService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IBasketService basketService,IUnitOfWork unitOfWork,IMapper mapper)
        {
            _basketService = basketService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<OrderDetailsDto> CreateOrderAsync(OrderDto input)
        {
            var basket=await _basketService.GetCustomerAsync(input.BasketId);
            if (basket is null)
                throw new Exception("Basket Not Exist");
            var orderItems = new List<OrderItemDto>();
            foreach (var item in basket.BaketItems)
            {
                var productitem = await _unitOfWork.Repository<Product, int>().GetByIdAsync(item.ProductId);
                if (productitem is null)
                    throw new Exception($"Product With Id {item.ProductId} Not Exist");

                var itemOrdered = new ProuctItem
                {
                    PictureUrl = productitem.PictureUrl,
                    ProductId = productitem.Id,
                    ProductName = productitem.Name,
                };
                var orderIten = new OrderItem
                {
                    Price = productitem.Price,
                    Quantity = item.Quantity,
                    ProductItem = itemOrdered

                };
                var mappedOrderItem = _mapper.Map<OrderItemDto>(orderItems);
                orderItems.Add(mappedOrderItem);
            }
                var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod, int>().GetByIdAsync(input.DeliveryMethodId);
                if (deliveryMethod is null)
                    throw new Exception("Delivery Method not Provided");
                var subtotal=orderItems.Sum(x=>x.Quantity*x.Price);

                var mappedShiipingAddress = _mapper.Map<ShippingAddress>(input.ShippingAddress);
                var mappedOrderItems=_mapper.Map<List<OrderItem>>(orderItems);
                var order = new Order
                {
                    DeliveryMethodId = deliveryMethod.Id,
                    ShippingAddress = mappedShiipingAddress,
                    EmailBuyer = input.EmailBuyer,
                    BasketId = input.BasketId,
                    OrderItems = mappedOrderItems,
                    SubTotal = subtotal,
                };
                await _unitOfWork.Repository<Order, int>().AddAsync(order);
                await _unitOfWork.CompleteAsync();

                var mappedorder=_mapper.Map<OrderDetailsDto> (order);

                return mappedorder;
            

        }

        public async Task<IReadOnlyList<DeliveryMethod>>GetAllDeliveryMethodAsync()
        =>await _unitOfWork.Repository<DeliveryMethod, int>().GetAllAsync();  

        

        public async Task<IReadOnlyList<OrderDetailsDto>> GetAllOrdersForUserAsync(string BuyerEmail)
        {
            var specs = new OrderWithItemSpecification(BuyerEmail);
            var orders=await _unitOfWork.Repository<Order,int>().GetAllwithSpecficicationAsync(specs);
            if (orders is { Count: <= 0 })
                throw new Exception("You Donot have any orders yet");
            var mappedOrer=_mapper.Map<List<OrderDetailsDto>> (orders);
            return mappedOrer;
        }

        public async Task<IReadOnlyList<OrderDetailsDto>> GetOrdersByIdAsync(int Id)
        {
            var specs = new OrderWithItemSpecification(Id);
            var order = await _unitOfWork.Repository<Order, int>().GetByIdSpecificationsAsync(specs);
            if (order is null )
                throw new Exception($"You not  have any orders with id {Id} ");
            var mappedOrer =  _mapper.Map<List<OrderDetailsDto>>(order);
            return mappedOrer;
        }
 
    }
}
