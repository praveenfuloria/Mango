using Mango.Services.AuthAPI.Models.DTO;
using Mango.Services.AuthAPI.Service.Iservice;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly ResponceDto responceDto;

        public AuthAPIController(IAuthService authService)
        {
            this.authService = authService;
            responceDto = new ResponceDto();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto registrationRequestDto)
        {
            var errorMessage = await authService.RegisterAsync(registrationRequestDto);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                responceDto.isSuccess = false;
                responceDto.Message= errorMessage;
                return BadRequest(responceDto);
            }

            return Ok(responceDto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login()
        {
            return Ok("Login API");
        }
    }
}
