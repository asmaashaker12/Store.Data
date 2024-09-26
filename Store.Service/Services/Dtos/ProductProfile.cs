using AutoMapper;
using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.Dtos
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Product,ProductDetailsDto>()
                .ForMember(des=>des.BrandName,options=>options.MapFrom(src=>src.ProductBrand.Name))
                .ForMember(des=>des.TypeName,options=>options.MapFrom(src=>src.ProductType.Name))
                .ForMember(des=>des.PictureUrl, options=>options.MapFrom<ProductServiceUrlResolver>());
            CreateMap<ProductBrand, BrandTypeDetailsDto>();
            CreateMap<ProductType, BrandTypeDetailsDto>();
        }
    }
}
