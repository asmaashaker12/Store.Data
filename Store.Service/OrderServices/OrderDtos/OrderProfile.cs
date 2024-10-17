using AutoMapper;
using Store.Data.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.OrderServices.OrderDtos
{
    public class OrderProfile:Profile
    {
        public OrderProfile()
        {
            CreateMap<ShippingAddress,AddressDto>().ReverseMap();
            CreateMap<Order, OrderDetailsDto>()
             .ForMember(des => des.DeliveryMethodName, options => options.MapFrom(src => src.DeliveryMethod.ShortName))
             .ForMember(des => des.ShippingPrice, options => options.MapFrom(src => src.DeliveryMethod.Price));

            CreateMap<OrderItem, OrderItemDto>()
           .ForMember(des => des.ProductId, options => options.MapFrom(src => src.ProductItem.ProductId))
           .ForMember(des => des.ProductName, options => options.MapFrom(src => src.ProductItem.ProductName))
            .ForMember(des => des.ProductName, options => options.MapFrom(src => src.ProductItem.PictureUrl))
           .ForMember(des => des.PictureUrl, options => options.MapFrom<OrderItemPictureUrlResolver>()).ReverseMap();
        }
    }
}
