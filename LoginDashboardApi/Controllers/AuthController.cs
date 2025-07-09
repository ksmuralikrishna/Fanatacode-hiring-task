using Microsoft.AspNetCore.Mvc;
using LoginDashboardApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LoginDashboardApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        // Hardcoded user list
        private List<User> users = new List<User>
        {
            new User { Username = "admin", Password = "1234" }
        };

        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            var validUser = users.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);
            if (validUser == null)
                return Unauthorized("Invalid username or password");

            var token = GenerateJwtToken(validUser.Username);
            return Ok(new { token });
        }

        private string GenerateJwtToken(string username)
        {
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                                  SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}