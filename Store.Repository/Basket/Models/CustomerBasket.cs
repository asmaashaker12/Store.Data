﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Basket.Models
{
    public class CustomerBasket
    {
        public string? Id { get; set; }
        public int? DeliveryMethod { get; set; }
        public decimal ShippingPrice { get; set; }
        public List<BaketItem> BaketItems { get; set; } = new List<BaketItem>();
        public string? PaymentIntendId { get; set; }
        public string? ClientSecret { get; set; }
    }
}
