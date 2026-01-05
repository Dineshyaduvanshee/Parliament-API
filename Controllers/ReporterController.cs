using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parliament_API.Data;
using Parliament_API.Models;

namespace Parliament_API.Controllers
{
    [Route("api/reporter")]
    [ApiController]
    public class ReporterController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReporterController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ GET: api/reporter
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReporterProfile>>> GetAll()
        {
            return await _context.ReporterProfile
                .OrderByDescending(r => r.Id)
                .ToListAsync();
        }

        // ✅ GET: api/reporter/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ReporterProfile>> GetById(int id)
        {
            var reporter = await _context.ReporterProfile.FindAsync(id);
            if (reporter == null)
                return NotFound("Reporter not found");

            return reporter;
        }

        // ✅ SEARCH: api/reporter/search?city=Delhi&beat=Politics
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ReporterProfile>>> Search(
            [FromQuery] string? city,
            [FromQuery] string? beat,
            [FromQuery] string? mediaHouse)
        {
            var query = _context.ReporterProfile.AsQueryable();

            if (!string.IsNullOrWhiteSpace(city))
                query = query.Where(r => r.City == city);

            if (!string.IsNullOrWhiteSpace(beat))
                query = query.Where(r => r.Beat == beat);

            if (!string.IsNullOrWhiteSpace(mediaHouse))
                query = query.Where(r => r.MediaHouse == mediaHouse);

            return await query.ToListAsync();
        }

        // ✅ POST: api/reporter
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReporterProfile reporter)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.ReporterProfile.Add(reporter);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Reporter created successfully",
                reporter
            });
        }

        // ✅ PUT: api/reporter/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] ReporterProfile reporter)
        {
            var existing = await _context.ReporterProfile.FindAsync(id);
            if (existing == null)
                return NotFound("Reporter not found");

            // 🔁 Field-by-field update (SAFE)
            existing.Name = reporter.Name;
            existing.MediaHouse = reporter.MediaHouse;
            existing.Beat = reporter.Beat;
            existing.City = reporter.City;
            existing.State = reporter.State;
            existing.Email = reporter.Email;
            existing.Phone = reporter.Phone;
            existing.ExperienceYears = reporter.ExperienceYears;
            existing.Website = reporter.Website;
            existing.PhotoUrl = reporter.PhotoUrl;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Reporter updated successfully"
            });
        }

        // ✅ DELETE: api/reporter/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var reporter = await _context.ReporterProfile.FindAsync(id);
            if (reporter == null)
                return NotFound("Reporter not found");

            _context.ReporterProfile.Remove(reporter);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Reporter deleted successfully"
            });
        }
    }
}
