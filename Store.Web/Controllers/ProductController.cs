using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Store.Service.Services;
using Store.Service.Services.Dtos;

namespace Store.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
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
        public async Task<ActionResult<IReadOnlyList<ProductDetailsDto>>> GetAllProducts()
     => Ok(await _productService.GetAllProductsAsync());
        [HttpGet("{Id}")]
        public async Task<ActionResult<ProductDetailsDto>> GetProductById(int?Id)
  => Ok(await _productService.GetProductByIdAsync(Id));
    }
}
