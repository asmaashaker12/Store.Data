using AutoMapper;
using Store.Data.Entities;
using Store.Repository.Interfaces;
using Store.Repository.Specification;
using Store.Repository.Specification.Product;
using Store.Service.Helper;
using Store.Service.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.Repository<ProductBrand, int>().GetAllAsync();
            var mappedBrand=_mapper.Map<IReadOnlyList< BrandTypeDetailsDto>>(brands);
            //IReadOnlyList<BrandTypeDetailsDto> mappedBrand = brands.Select(x => new BrandTypeDetailsDto
            //{
            //    Id = x.Id,
            //    Name = x.Name,
            //    CreatAt = x.CreateTime
            //}).ToList();
            return mappedBrand; ;
        }

        public async Task<PaginatedResultDto<ProductDetailsDto>> GetAllProductsAsync(ProductSpecification input)
        {
            var specs =new ProductWithSpecifcations(input);
            var products = await _unitOfWork.Repository<Product,int>().GetAllwithSpecficicationAsync(specs);
            var countSpecs = new ProductWithCountSpecification(input);
            var count = await _unitOfWork.Repository<Product, int>().GetCountSpecficicationAsync(countSpecs);
            var mappedProducts =_mapper.Map<IReadOnlyList<ProductDetailsDto>>(products);
            //    = products.Select(x => new ProductDetailsDto
            //{
            //    Id=x.Id,
            //    Name = x.Name,
            //    Description = x.Description,
            //    PictureUrl=x.PictureUrl,
            //    Price = x.Price,
            //    BrandName=x.ProductBrand.Name,
            //    TypeName=x.ProductType.Name,
            //    CreatAt=x.CreateTime
            //}).ToList();
            return new PaginatedResultDto<ProductDetailsDto>(input.PageIindex,input.PageSize,count,mappedProducts);
        }

       public  async Task<IReadOnlyList<ProductDetailsDto>>GetAllTypesAsync()
        {
            var products = await _unitOfWork.Repository<ProductType, int>().GetAllAsync();
            var mappedProducts = _mapper.Map<IReadOnlyList<ProductDetailsDto>>(products);
            //= products.Select(x => new ProductDetailsDto
            //{
            //    Id = x.Id,
            //    Name = x.Name,
            //    CreatAt = x.CreateTime
            //}).ToList();
            return mappedProducts;
        }

       public async Task<ProductDetailsDto> GetProductByIdAsync(int? ProductId)
        {
            if(ProductId is null) 
                throw new Exception("Product is null");
            var productspec=new ProductWithSpecifcations(ProductId);
            var product = await _unitOfWork.Repository<Product, int>().GetByIdSpecificationsAsync(productspec);
            if (product is null)
                throw new Exception("Product Not Found");
            var mappedProductDetails = _mapper.Map<ProductDetailsDto>(product);
            //    = new ProductDetailsDto 
            //{
            //    Id=product.Id,
            //    Name=product.Name,
            //    Description=product.Description,
            //    BrandName = product.ProductBrand.Name,
            //    TypeName=product.ProductType.Name,
            //    CreatAt=product.CreateTime,
            //    Price=product.Price,
            //    PictureUrl=product.PictureUrl,
            //};
            return mappedProductDetails;

        }

        
    }
}
