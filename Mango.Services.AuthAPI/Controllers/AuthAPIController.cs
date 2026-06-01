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
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var result = await authService.LoginAsync(loginRequestDto);
            if(result.User == null)
            {
                responceDto.isSuccess = false;
                responceDto.Message = "Username or password is incorrect.";
                return BadRequest(responceDto);
            }
            responceDto.Result = result;
            return Ok(responceDto);
        }

        [HttpPost("assignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDto registrationRequestDto)
        {
            var result = await authService.AssignRole(registrationRequestDto.Email,registrationRequestDto.Role);
            if (!result)
            {
                responceDto.isSuccess = false;
                responceDto.Message = "Something Went wrong.";
                return BadRequest(responceDto);
            }
            responceDto.isSuccess = result;
            return Ok(responceDto);
        }
    }
}
