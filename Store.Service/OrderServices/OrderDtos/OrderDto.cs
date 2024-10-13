using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.OrderServices.OrderDtos
{
    public class OrderDto
    {
        public string BasketId { get; set; }
        public string BuyerName { get; set; }
        public string EmailBuyer { get; set; }
        [Required]
        public int DeliveryMethodId { get; set; }
        public AddressDto ShippingAddress { get; set; }
    }
}
