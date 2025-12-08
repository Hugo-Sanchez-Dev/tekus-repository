#region Usings
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tekus.Providers.Application.Interfaces.Auth;
using Tekus.Providers.Domain.Entities.Auth;
#endregion

namespace Tekus.Providers.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        #region Constants
        private const string USERNAME = "admin";
        private const string PASSWORD = "tekusTest";
        #endregion

        #region Instance
        private readonly IAuthService _authService;
        #endregion

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public IActionResult Login([FromBody] Auth request)
        {
            if (request.User == USERNAME && request.Password == PASSWORD)
            {
                string token = _authService.GenerateJwtToken();
                return Ok(new { token });
            }

            return Unauthorized(new { message = "Invalid credentials" });
        }
    }
}
