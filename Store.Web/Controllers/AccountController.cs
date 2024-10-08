using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Service.HandleResponse;
using Store.Service.UserServices;
using Store.Service.UserServices.Dtos;

namespace Store.Web.Controllers
{
  
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
           _userService = userService;
        }
        [HttpPost]
        public async Task<ActionResult<UserDto>>Login(LoginDto loginDto)
        {
            var user = await _userService.Login(loginDto);
            if (user == null)
                return BadRequest(new CustomException(400, "Email not Exist"));
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Redister(RegisterDto loginDto)
        {
            var user = await _userService.Register(loginDto);
            if (user == null)
                return BadRequest(new CustomException(400, "Email Already Exist"));
            return Ok(user);
        }
    }
}
