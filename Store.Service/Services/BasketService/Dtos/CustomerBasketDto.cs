﻿using Store.Repository.Basket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.BasketService.Dtos
{
    public class CustomerBasketDto
    {
        public string? Id { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal ShippingPrice { get; set; }
        public List<BaketItemDto> BaketItems { get; set; } = new List<BaketItemDto>();
        public string? PaymentIntendId { get; set; }
        public string? ClientSecret { get; set; }

    }
}
