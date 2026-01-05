using Microsoft.AspNetCore.Mvc;
using Parliament_API.Data;
using Parliament_API.Models;

namespace Parliament_API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET ALL USERS
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Users
                .Select(u => new { u.Id, u.Username, u.Role })
                .ToList());
        }

        // GET USER BY ID
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();

            return Ok(new { user.Id, user.Username, user.Role });
        }

        // UPDATE USER
        [HttpPut("{id}")]
        public IActionResult Update(int id, User model)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();

            user.Username = model.Username;
            user.Role = model.Role;

            if (!string.IsNullOrEmpty(model.PasswordHash))
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.PasswordHash);
            }

            _context.SaveChanges();
            return Ok("User updated");
        }

        // DELETE USER
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();

            _context.Users.Remove(user);
            _context.SaveChanges();

            return Ok("User deleted");
        }
    }
}
