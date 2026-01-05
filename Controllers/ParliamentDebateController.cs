using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parliament_API.Data;
using Parliament_API.Models;

namespace Parliament_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParliamentDebateController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ParliamentDebateController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ParliamentDebate
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParliamentDebateTopic>>> GetAll()
        {
            return await _context.ParliamentDebateTopics.ToListAsync();
        }

        // GET: api/ParliamentDebate/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ParliamentDebateTopic>> GetById(int id)
        {
            var topic = await _context.ParliamentDebateTopics.FindAsync(id);

            if (topic == null)
                return NotFound();

            return topic;
        }

        // GET: api/ParliamentDebate/search?topic=Education&status=Ongoing
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ParliamentDebateTopic>>> Search(
            string? topic,
            string? house,
            string? status)
        {
            var query = _context.ParliamentDebateTopics.AsQueryable();

            if (!string.IsNullOrEmpty(topic))
                query = query.Where(t => t.TopicName.Contains(topic));

            if (!string.IsNullOrEmpty(house))
                query = query.Where(t => t.House == house);

            if (!string.IsNullOrEmpty(status))
                query = query.Where(t => t.Status == status);

            return await query.ToListAsync();
        }

        // POST: api/ParliamentDebate
        [HttpPost]
        public async Task<ActionResult<ParliamentDebateTopic>> Create(ParliamentDebateTopic topic)
        {
            _context.ParliamentDebateTopics.Add(topic);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = topic.Id }, topic);
        }

        // PUT: api/ParliamentDebate/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ParliamentDebateTopic topic)
        {
            if (id != topic.Id)
                return BadRequest("ID mismatch");

            _context.Entry(topic).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/ParliamentDebate/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var topic = await _context.ParliamentDebateTopics.FindAsync(id);

            if (topic == null)
                return NotFound();

            _context.ParliamentDebateTopics.Remove(topic);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
