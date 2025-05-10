using Microsoft.AspNetCore.Identity;

namespace EgyWalk.Api.Repositories.TokenRepository
{
    public interface ITokenRepo
    {


        string CreateToken(IdentityUser user , List<string> roles);

    }
}
