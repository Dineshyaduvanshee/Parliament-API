using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parliament_API.Data;
using Parliament_API.Models;

namespace Parliament_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PoliceController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PoliceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Police
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PoliceOfficer>>> GetAll()
        {
            return await _context.PoliceOfficers.ToListAsync();
        }

        // GET: api/Police/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PoliceOfficer>> GetById(int id)
        {
            var officer = await _context.PoliceOfficers.FindAsync(id);

            if (officer == null)
                return NotFound();

            return officer;
        }

        // GET: api/Police/search?station=Hazratganj&rank=Inspector
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<PoliceOfficer>>> Search(
            string? station,
            string? rank,
            string? department)
        {
            var query = _context.PoliceOfficers.AsQueryable();

            if (!string.IsNullOrEmpty(station))
                query = query.Where(o => o.PoliceStation == station);

            if (!string.IsNullOrEmpty(rank))
                query = query.Where(o => o.Rank == rank);

            if (!string.IsNullOrEmpty(department))
                query = query.Where(o => o.Department == department);

            return await query.ToListAsync();
        }

        // POST: api/Police
        [HttpPost]
        public async Task<ActionResult<PoliceOfficer>> Create(PoliceOfficer officer)
        {
            _context.PoliceOfficers.Add(officer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = officer.Id }, officer);
        }

        // PUT: api/Police/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PoliceOfficer officer)
        {
            if (id != officer.Id)
                return BadRequest("ID mismatch");

            _context.Entry(officer).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Police/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var officer = await _context.PoliceOfficers.FindAsync(id);

            if (officer == null)
                return NotFound();

            _context.PoliceOfficers.Remove(officer);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
