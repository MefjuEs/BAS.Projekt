using BAS.AppServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BAS.Projekt.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService authService;

        public AccountController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LogIn([FromBody] LogInDTO logInDTO)
        {
            var result = await this.authService.LogIn(logInDTO);
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            var result = await this.authService.Register(registerDTO);
            return Ok(result);
        }

        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var result = await this.authService.ConfirmRegistration(userId, token);
            return Ok(result);
        }
    }
}
