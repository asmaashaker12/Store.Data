using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Store.Repository.Specification.Product;
using Store.Service.Services;
using Store.Service.Services.Dtos;
using Store.Web.Helper;

namespace Store.Web.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<BrandTypeDetailsDto>>> GetAllBrands()
            => Ok(await _productService.GetAllBrandsAsync());
        [HttpGet("GetAllTypes")]
        public async Task<ActionResult<IReadOnlyList<BrandTypeDetailsDto>>> GetAllTypes()
          => Ok(await _productService.GetAllTypesAsync()); 
        [HttpGet("GetAllProducts")]
        [Cash(10)]
        public async Task<ActionResult<IReadOnlyList<ProductDetailsDto>>> GetAllProducts([FromQuery]ProductSpecification specs)
     => Ok(await _productService.GetAllProductsAsync(specs));
        [HttpGet("{Id}")]
        public async Task<ActionResult<ProductDetailsDto>> GetProductById(int?Id)
  => Ok(await _productService.GetProductByIdAsync(Id));
    }
}
