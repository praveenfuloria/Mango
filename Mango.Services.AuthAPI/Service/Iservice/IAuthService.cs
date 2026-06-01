using Mango.Services.AuthAPI.Models.DTO;

namespace Mango.Services.AuthAPI.Service.Iservice
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegistrationRequestDto registrationRequestDto);

        Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
    }
}
