using Microsoft.AspNetCore.Mvc;
using SafeQuake.Application.Interfaces.User;
using SafeQuake.Domain.Entities;

namespace SafeQuake.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IGetAllUsersUseCase _getAllUsersUseCase;

        public AuthController(IGetAllUsersUseCase getAllUsersUseCase)
        {
            _getAllUsersUseCase = getAllUsersUseCase;
        }

        /// <summary>
        /// Autentica um usuário
        /// </summary>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {       
            var users = await _getAllUsersUseCase.ExecuteAsync();
            var user = users.FirstOrDefault(u => 
                u.Email.Equals(request.Email, StringComparison.OrdinalIgnoreCase) && 
                u.Password == request.Password);

            if (user == null)
                return Unauthorized(new { message = "Email ou senha inválidos" });

            return Ok(user);
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
} 