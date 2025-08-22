using Microsoft.AspNetCore.Mvc;
using LoginDashboardApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LoginDashboardApi.DTO;
using LoginDashboardApi.Data;

namespace LoginDashboardApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly userDbContext _context;

        //public AuthController(IConfiguration config)
        //{
        //    _config = config;
        //}

        public AuthController(userDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Username == request.Username && u.Password == request.Password);
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid username or password" });

            }
            return Ok(new { message = "Login Successful" });
        
        }
    }
}