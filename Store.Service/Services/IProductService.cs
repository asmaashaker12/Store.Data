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
        Task<IReadOnlyList<ProductDetailsDto>> GetAllProductsAsync();
        Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllBrandsAsync();
        Task<IReadOnlyList<ProductDetailsDto>> GetAllTypesAsync();
    }
}
