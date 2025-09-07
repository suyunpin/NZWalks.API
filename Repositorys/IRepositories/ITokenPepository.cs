using Microsoft.AspNetCore.Identity;

namespace NZWalks.API.Repositorys.IRepositories
{
    public interface ITokenPepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
