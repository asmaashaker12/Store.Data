using Store.Data.Entities;
using Store.Service.OrderServices.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.OrderServices
{
    public interface IOrderService
    {
        Task<OrderDetailsDto>CreateOrderAsync(OrderDto  input);
        Task<IReadOnlyList<OrderDetailsDto>>GetAllOrdersForUserAsync(string BuyerEmail);
        Task<IReadOnlyList<OrderDetailsDto>>GetOrdersByIdAsync(int Id);
        Task<IReadOnlyList<DeliveryMethod>>GetAllDeliveryMethodAsync();
    }
}
