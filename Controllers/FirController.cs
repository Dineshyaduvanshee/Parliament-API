using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parliament_API.Data;
using Parliament_API.Models;

namespace Parliament_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FirController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FirController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Fir
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fir>>> GetAll()
        {
            return await _context.Firs.ToListAsync();
        }

        // GET: api/Fir/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Fir>> GetById(int id)
        {
            var fir = await _context.Firs.FindAsync(id);
            if (fir == null) return NotFound();
            return fir;
        }

        // GET: api/Fir/search?firNumber=FIR-2025-001&status=Registered
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Fir>>> Search(
            string? firNumber,
            string? status,
            string? policeStation)
        {
            var query = _context.Firs.AsQueryable();

            if (!string.IsNullOrEmpty(firNumber))
                query = query.Where(f => f.FirNumber == firNumber);

            if (!string.IsNullOrEmpty(status))
                query = query.Where(f => f.Status == status);

            if (!string.IsNullOrEmpty(policeStation))
                query = query.Where(f => f.PoliceStation == policeStation);

            return await query.ToListAsync();
        }

        // POST: api/Fir
        [HttpPost]
        public async Task<ActionResult<Fir>> Create(Fir fir)
        {
            _context.Firs.Add(fir);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = fir.Id }, fir);
        }

        // PUT: api/Fir/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Fir fir)
        {
            if (id != fir.Id)
                return BadRequest("ID mismatch");

            _context.Entry(fir).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Fir/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var fir = await _context.Firs.FindAsync(id);
            if (fir == null) return NotFound();

            _context.Firs.Remove(fir);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
