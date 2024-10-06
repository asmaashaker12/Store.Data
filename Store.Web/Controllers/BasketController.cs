using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Service.Services.BasketService;
using Store.Service.Services.BasketService.Dtos;

namespace Store.Web.Controllers
{
    public class BasketController : BaseController
    {
        private readonly IBasketService _basketService;
        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerBasketDto>> GetBasketAsync(string Id)
            => Ok(await _basketService.GetCustomerAsync(Id));

        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> UpdateBasketAsync(CustomerBasketDto  customerBasketDto)
           => Ok(await _basketService.UpdateBasketAsync(customerBasketDto));
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteBasketAsync(string Id)
           => Ok(await _basketService.DeleteBasketAsync(Id));
    }

}
