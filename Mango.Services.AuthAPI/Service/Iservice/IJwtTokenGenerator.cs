using Mango.Services.AuthAPI.Models;

namespace Mango.Services.AuthAPI.Service.Iservice
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser);
    }
}
