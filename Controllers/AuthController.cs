using Microsoft.AspNetCore.Mvc;
using Parliament_API.Data;
using Parliament_API.Models;

namespace Parliament_API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        // REGISTER
        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            if (_context.Users.Any(x => x.Username == user.Username))
                return BadRequest("Username already exists");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            user.Role = "User";

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok("User registered successfully");
        }

        // LOGIN
        [HttpPost("login")]
        public IActionResult Login(User login)
        {
            var user = _context.Users.SingleOrDefault(x => x.Username == login.Username);

            if (user == null ||
                !BCrypt.Net.BCrypt.Verify(login.PasswordHash, user.PasswordHash))
                return Unauthorized("Invalid username or password");

            return Ok(new
            {
                username = user.Username,
                role = user.Role,
                token = "JWT_TOKEN_WILL_BE_GENERATED_HERE"
            });
        }
    }
}
