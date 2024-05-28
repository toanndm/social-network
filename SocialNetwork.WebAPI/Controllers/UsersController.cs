using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Application.Services.Auths;
using SocialNetwork.Application.Services.Auths.DTOs;

namespace SocialNetwork.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IAuthenticationService _authService;

        public UsersController(IAuthenticationService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            var result = await _authService.Register(registerRequest);
            return new ObjectResult(result)
            {
                StatusCode = result.Code
            };
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var result = await _authService.Login(loginRequest);
            return new ObjectResult(result)
            {
                StatusCode = result.Code
            };
        }
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(RefreshTokenDto refreshToken)
        {
            var result = await _authService.Refresh(refreshToken);
            return new ObjectResult(result)
            {
                StatusCode = result.Code
            };
        }
    }
}
