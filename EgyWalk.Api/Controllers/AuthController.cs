using EgyWalk.Api.Dtos.AuthDtos;
using EgyWalk.Api.Repositories.TokenRepository;
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
        private readonly ITokenRepo _tokenRepo;

        public AuthController(UserManager<IdentityUser> userManager , ITokenRepo tokenRepo)
        {
            _userManager = userManager;
            _tokenRepo = tokenRepo;
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



        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto LoginDto)
        {


            var user = await _userManager.FindByEmailAsync(LoginDto.UserName);


            if (user != null)
            {

                var CheakPassword = await _userManager.CheckPasswordAsync(user, LoginDto.Password);
                if (CheakPassword)
                {
                    // cheak user roles 
                    var UserRoles = await _userManager.GetRolesAsync(user);
                    if (UserRoles != null )
                    {

                        // jwt token 

                        var token = _tokenRepo.CreateToken(user, UserRoles.ToList());


                        var response = new LoginResponseDto()
                        {
                            JwtToken = token
                        };

                        return Ok(response.JwtToken);
                    }


                        
                    
                }

            }





            return BadRequest("Email Or Password was Wrong ");
        }
    }
}
