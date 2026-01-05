using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parliament_API.Data;
using Parliament_API.Models;

namespace Parliament_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CriminalController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CriminalController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Criminal
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CriminalProfile>>> GetAll()
        {
            return await _context.CriminalProfiles.ToListAsync();
        }

        // GET: api/Criminal/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CriminalProfile>> GetById(int id)
        {
            var criminal = await _context.CriminalProfiles.FindAsync(id);

            if (criminal == null)
                return NotFound();

            return criminal;
        }

        // GET: api/Criminal/search?crimeType=Robbery&status=Wanted
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<CriminalProfile>>> Search(
            string? crimeType,
            string? status,
            string? state)
        {
            var query = _context.CriminalProfiles.AsQueryable();

            if (!string.IsNullOrEmpty(crimeType))
                query = query.Where(c => c.CrimeType.Contains(crimeType));

            if (!string.IsNullOrEmpty(status))
                query = query.Where(c => c.Status == status);

            if (!string.IsNullOrEmpty(state))
                query = query.Where(c => c.State == state);

            return await query.ToListAsync();
        }

        // POST: api/Criminal
        [HttpPost]
        public async Task<ActionResult<CriminalProfile>> Create(CriminalProfile criminal)
        {
            _context.CriminalProfiles.Add(criminal);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = criminal.Id }, criminal);
        }

        // PUT: api/Criminal/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CriminalProfile criminal)
        {
            if (id != criminal.Id)
                return BadRequest("ID mismatch");

            _context.Entry(criminal).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Criminal/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var criminal = await _context.CriminalProfiles.FindAsync(id);

            if (criminal == null)
                return NotFound();

            _context.CriminalProfiles.Remove(criminal);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
