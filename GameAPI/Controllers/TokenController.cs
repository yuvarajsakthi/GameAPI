using GameAPI.DTOs;
using GameAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IUser _userRepository;
        private readonly IToken _tokenService;

        public TokenController(IUser userRepository, IToken tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        // POST: api/Token/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            // Find user by email
            var user = await _userRepository.GetByEmailAsync(login.Email);
            if (user == null || user.Password != login.Password)
                return Unauthorized("Invalid email or password");

            // Generate token with user roles
            var roles = new List<string> { user.Role ?? "Viewer" }; // default to Viewer if null
            var token = _tokenService.GenerateToken(user, roles);

            return Ok(new
            {
                Token = token,
                User = new UserDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Role = user.Role
                }
            });
        }
    }

    // DTO for login request
    public class LoginDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
