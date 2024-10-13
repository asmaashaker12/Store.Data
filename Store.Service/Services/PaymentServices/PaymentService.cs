using AutoMapper;
using Microsoft.Extensions.Configuration;
using Store.Data.Entities;
using Store.Data.Entities.OrderEntities;
using Store.Repository.Interfaces;
using Store.Repository.Specification.OrderSpecs;
using Store.Service.OrderServices.OrderDtos;
using Store.Service.Services.BasketService;
using Store.Service.Services.BasketService.Dtos;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.PaymentServices
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketService _basketService;
        private readonly IMapper _mapper;

        public PaymentService(IConfiguration configuration,IUnitOfWork unitOfWork,IBasketService basketService,IMapper mapper)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _basketService = basketService;
           _mapper = mapper;
        }
        public async Task<CustomerBasketDto> CreateOrUpdatePaymentIntent(CustomerBasketDto input)
        {
            StripeConfiguration.ApiKey = _configuration["Publishablekey:Stripe"];
            if (input == null)
                throw new Exception("not Exist");
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod, int>().GetByIdAsync(input.DeliveryMethodId.Value);
            if (deliveryMethod is null)
                throw new Exception("Delivery Method not Provided");
            decimal shippingPrice = deliveryMethod.Price;
            foreach (var item in input.BaketItems)
            {
                var product = await _unitOfWork.Repository<Data.Entities.Product, int>().GetByIdAsync(item.ProductId);
                if (item.Price != product.Price)
                    item.Price = product.Price;
            }
            var service = new PaymentIntentService();
            PaymentIntent paymentIntent = new PaymentIntent();
            if (string.IsNullOrEmpty(input.PaymentIntendId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)input.BaketItems.Sum(x => x.Quantity * (x.Price * 100)) + (long)(input.ShippingPrice * 100),
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }

                };
                paymentIntent = await service.CreateAsync(options);
                input.PaymentIntendId = paymentIntent.Id;
                input.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                var option = new PaymentIntentUpdateOptions
                {
                    Amount = (long)input.BaketItems.Sum(x => x.Quantity * (x.Price * 100)) + (long)(input.ShippingPrice * 100)
                };
                await service.UpdateAsync(input.PaymentIntendId, option);

            }
            await _basketService.UpdateBasketAsync(input);
            return input;
        }

        public async Task<OrderDetailsDto> UpdateOrderPaymentFailed(string PaymentIntenId)
        {
            var specs=new OrderWithPaymentIntendSpecification(PaymentIntenId);
            var order= await _unitOfWork.Repository<Order,int>().GetByIdSpecificationsAsync(specs);
            if (order is null)
                throw new Exception("Order not Exist");
            order.OrderPaymentStatus = OrderPaymentStatus.Failed;
            _unitOfWork.Repository<Order,int>().UpdateAsync(order);
            await _unitOfWork.CompleteAsync();
            var mappedOrder=_mapper.Map<OrderDetailsDto>(order);
            return mappedOrder;

        }

        public async Task<OrderDetailsDto> UpdateOrderPaymentSucceeded(string PaymentIntenId)
        {
            var specs = new OrderWithPaymentIntendSpecification(PaymentIntenId);
            var order = await _unitOfWork.Repository<Order, int>().GetByIdSpecificationsAsync(specs);
            if (order is null)
                throw new Exception("Order not Exist");
            order.OrderPaymentStatus = OrderPaymentStatus.Received;
            _unitOfWork.Repository<Order, int>().UpdateAsync(order);
            await _unitOfWork.CompleteAsync();
            await _basketService.DeleteBasketAsync(order?.BasketId);
            var mappedOrder = _mapper.Map<OrderDetailsDto>(order);
            return mappedOrder;
        }
    }
}
