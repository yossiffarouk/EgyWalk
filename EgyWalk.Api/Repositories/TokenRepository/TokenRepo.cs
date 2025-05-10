using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EgyWalk.Api.Repositories.TokenRepository
{
    public class TokenRepo : ITokenRepo
    {
        private readonly IConfiguration _configuration;

        public TokenRepo(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string CreateToken(IdentityUser user, List<string> roles)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim (ClaimTypes.Email,user.Email));


            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            

            var Credential = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var JwtToken = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(100),
                signingCredentials: Credential


                );


            return new JwtSecurityTokenHandler().WriteToken(JwtToken);
        }
    }
}
