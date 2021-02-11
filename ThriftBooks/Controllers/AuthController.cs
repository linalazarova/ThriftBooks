using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThriftBooks.Business.Models.Users;
using ThriftBooks.Business.Services.Interfaces;

namespace ThriftBooks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var token = await _authService.Authenticate(model);

            if (token == null)
            {
                return BadRequest("Invalid username or password.");
            }

            return Ok(token);
        }
    }
}
