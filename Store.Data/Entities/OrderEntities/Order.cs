using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.Entities.OrderEntities
{
    public class Order:BaseEntity<int>
    {
        public string EmailBuyer { get; set; }
        public DateTimeOffset orderDate { get; set; }=DateTimeOffset.Now;
        public ShippingAddress ShippingAddress { get; set; }   
        public DeliveryMethod DeliveryMethod { get; set; }
        public int? DeliveryMethodId { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Placed;
        public OrderPaymentStatus  OrderPaymentStatus { get; set; } = OrderPaymentStatus.Pending;
        public decimal SubTotal { get; set; }
        public decimal GetTotal()
            => SubTotal + DeliveryMethod.Price;
        public IReadOnlyList<OrderItem> OrderItems { get; set; }
        public string? BasketId { get; set; }
        public string? PaymentIntendId { get; set; }

    }
}
