using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Store.Data.Entities;
using Store.Service.HandleResponse;
using Store.Service.UserServices;
using Store.Service.UserServices.Dtos;

namespace Store.Web.Controllers
{
  
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(IUserService userService,UserManager<AppUser> userManager)
        {
           _userService = userService;
            _userManager = userManager;
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
        public async Task<ActionResult<UserDto>> Register(RegisterDto loginDto)
        {
            var user = await _userService.Register(loginDto);
            if (user == null)
                return BadRequest(new CustomException(400, "Email Already Exist"));
            return Ok(user);
        }
        [HttpGet]
        [Authorize]
        public async Task<UserDto> GetUserDetails()
        {
            var userId = User?.FindFirst("UserId");
            var user =await _userManager.FindByIdAsync(userId.Value);
            return new UserDto
            {
                Id = Guid.Parse(user.Id),
                DisplayName = user.DisplayName,
                Email = user.Email,
            };
        }
    }
}
