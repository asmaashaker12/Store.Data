using AutoMapper;
using Store.Repository.Basket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.BasketService.Dtos
{
    public class BaskerProfile:Profile
    {
        public BaskerProfile()
        {
            CreateMap<CustomerBasket,CustomerBasketDto>().ReverseMap();
            CreateMap<BaketItem, BaketItemDto>().ReverseMap();
        }
    }
}
