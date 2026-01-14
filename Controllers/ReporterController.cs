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
            var reporters = await _context.ReporterProfile
                .OrderByDescending(r => r.Id)
                .ToListAsync();

            return Ok(reporters);
        }

        // ✅ GET: api/reporter/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ReporterProfile>> GetById(int id)
        {
            var reporter = await _context.ReporterProfile.FindAsync(id);

            if (reporter == null)
                return NotFound(new { message = "Reporter not found" });

            return Ok(reporter);
        }

        // ✅ SEARCH: api/reporter/search?city=Delhi&beat=Politics
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ReporterProfile>>> Search(
            [FromQuery] string? city,
            [FromQuery] string? beat,
            [FromQuery] string? mediaHouse)
        {
            IQueryable<ReporterProfile> query = _context.ReporterProfile;

            if (!string.IsNullOrWhiteSpace(city))
                query = query.Where(r => r.City!.ToLower() == city.ToLower());

            if (!string.IsNullOrWhiteSpace(beat))
                query = query.Where(r => r.Beat!.ToLower() == beat.ToLower());

            if (!string.IsNullOrWhiteSpace(mediaHouse))
                query = query.Where(r => r.MediaHouse.ToLower() == mediaHouse.ToLower());

            var result = await query
                .OrderByDescending(r => r.Id)
                .ToListAsync();

            return Ok(result);
        }

        // ✅ POST: api/reporter
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReporterProfile reporter)
        {
            if (reporter == null)
                return BadRequest(new { message = "Invalid payload" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.ReporterProfile.Add(reporter);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = reporter.Id },
                reporter
            );
        }

        // ✅ PUT: api/reporter/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] ReporterProfile reporter)
        {
            if (reporter == null)
                return BadRequest(new { message = "Invalid payload" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _context.ReporterProfile.FindAsync(id);
            if (existing == null)
                return NotFound(new { message = "Reporter not found" });

            // 🔁 Full field sync
            existing.Name = reporter.Name;
            existing.MediaHouse = reporter.MediaHouse;
            existing.Beat = reporter.Beat;
            existing.City = reporter.City;
            existing.State = reporter.State;
            existing.ExperienceYears = reporter.ExperienceYears;
            existing.Email = reporter.Email;
            existing.Phone = reporter.Phone;
            existing.Website = reporter.Website;
            existing.PhotoUrl = reporter.PhotoUrl;
            existing.Designation = reporter.Designation;
            existing.Education = reporter.Education;
            existing.TwitterHandle = reporter.TwitterHandle;
            existing.InstagramHandle = reporter.InstagramHandle;

            await _context.SaveChangesAsync();

            return Ok(existing);
        }

        // ✅ DELETE: api/reporter/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var reporter = await _context.ReporterProfile.FindAsync(id);
            if (reporter == null)
                return NotFound(new { message = "Reporter not found" });

            _context.ReporterProfile.Remove(reporter);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Reporter deleted successfully" });
        }
    }
}
