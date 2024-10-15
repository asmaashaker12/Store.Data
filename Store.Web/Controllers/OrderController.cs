using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Store.Data.Entities;
using Store.Service.HandleResponse;
using Store.Service.OrderServices;
using Store.Service.OrderServices.OrderDtos;
using System.Security.Claims;

namespace Store.Web.Controllers
{
    [Authorize]
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet]
        public async Task<ActionResult<OrderDetailsDto>>CreateOrderAsync(OrderDto input)
        {
            var order=await _orderService.CreateOrderAsync(input);
            if (order is null)
                return BadRequest(new Response(400, "error while creating Order"));
            return Ok(order);
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderDetailsDto>>> GetAllOrderforUsersAsync()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var orders=await _orderService.GetAllOrdersForUserAsync(email);
            if (orders is { Count: <= 0 })
                throw new Exception("Current user no Orders hae yet");

            return Ok(orders);
        }
        [HttpGet]

        public async Task<ActionResult<OrderDetailsDto>> GetAllOrderbyIdAsync(int ?Id,string buyeremail)
        {
            var email = User.FindFirstValue(buyeremail);
            var orders = await _orderService.GetOrdersByIdAsync(Id??0,email);
            if (orders is { Count: <= 0 })
                throw new Exception("Current user no Orders hae yet");

            return Ok(orders);
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethod()
        =>Ok(await _orderService.GetAllDeliveryMethodAsync());

        

    }
}
