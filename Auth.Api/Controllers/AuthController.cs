using Auth.Api.Services.Interfaces;
using Auth.Api.Services.Model;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        /// <summary>
        /// The login authenticate
        /// </summary>
        /// <param name="request">The request paramas</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(LoginDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [HttpPost("login")]
        public async Task<ActionResult<LoginDto>> Login([FromBody] LoginRequest request)
        {
            var response = await _authService.AuthenticateAsync(request.Username, request.Password);
            if (response == null)
                return Unauthorized("Invalid credentials");

            return Ok(response);
        }
    }
}
