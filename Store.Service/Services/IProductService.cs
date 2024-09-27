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
    public interface IProductService
    {
        Task<ProductDetailsDto> GetProductByIdAsync(int?ProductId);
        Task<PaginatedResultDto<ProductDetailsDto>> GetAllProductsAsync(ProductSpecification specs);
        Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllBrandsAsync();
        Task<IReadOnlyList<ProductDetailsDto>> GetAllTypesAsync();
    }
}
