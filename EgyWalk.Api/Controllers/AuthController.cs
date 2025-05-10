using EgyWalk.Api.Dtos.AuthDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EgyWalk.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        // register 
        [HttpPost]
        //[Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto RegisterDto)
        {

            var user = new IdentityUser
            {
                UserName = RegisterDto.UserName,
                Email = RegisterDto.UserName,

            };
            var identityreuslt = await _userManager.CreateAsync(user , RegisterDto.Password);


            if (identityreuslt.Succeeded)
            {
                if (RegisterDto.Rols!= null && RegisterDto.Rols.Any())
                {
                 var identityRole =   await _userManager.AddToRolesAsync(user, RegisterDto.Rols);
                    if (identityRole.Succeeded)
                    {
                        return Ok("User Has register ! pls Login !");
                    }
                }
                
            }





            return  BadRequest("Somthing want wrong ! ");
        }


    }
}
