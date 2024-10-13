using Store.Data.Entities.OrderEntities;
using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.OrderServices.OrderDtos
{
    public class OrderDetailsDto
    {
        public string EmailBuyer { get; set; }
        public DateTimeOffset orderDate { get; set; } = DateTimeOffset.Now;
        public ShippingAddress ShippingAddress { get; set; }
        public string DeliveryMethodName { get; set; }
        public int? DeliveryMethodId { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Placed;
        public OrderPaymentStatus OrderPaymentStatus { get; set; } = OrderPaymentStatus.Pending;
        public decimal SubTotal { get; set; }
        public decimal ShippingPrice { get; set; }
        public IReadOnlyList<OrderItem> OrderItems { get; set; }
        public string? BasketId { get; set; }
        public string? ClientSecret { get; set; }
    }
}
