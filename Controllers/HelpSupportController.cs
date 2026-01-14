using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parliament_API.Data;
using Parliament_API.Models;

namespace Parliament_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelpSupportController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HelpSupportController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ GET: api/HelpSupport
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _context.HelpSupports
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();

            return Ok(data);
        }

        // ✅ GET: api/HelpSupport/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _context.HelpSupports.FindAsync(id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        // ✅ POST: api/HelpSupport
        [HttpPost]
        public async Task<IActionResult> Create(HelpSupport model)
        {
            model.CreatedDate = DateTime.UtcNow;
            model.Status = "Open";

            _context.HelpSupports.Add(model);
            await _context.SaveChangesAsync();

            return Ok(model);
        }

        // ✅ PUT: api/HelpSupport/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, HelpSupport model)
        {
            if (id != model.Id)
                return BadRequest("ID mismatch");

            var existing = await _context.HelpSupports.FindAsync(id);

            if (existing == null)
                return NotFound();

            existing.Name = model.Name;
            existing.Email = model.Email;
            existing.Message = model.Message;
            existing.Status = model.Status;
            existing.PhotoUrl = model.PhotoUrl;

            await _context.SaveChangesAsync();

            return Ok(existing);
        }

        // ✅ DELETE: api/HelpSupport/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.HelpSupports.FindAsync(id);

            if (item == null)
                return NotFound();

            _context.HelpSupports.Remove(item);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Deleted successfully" });
        }
    }
}
