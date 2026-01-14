using Microsoft.AspNetCore.Mvc;
using Parliament_API.Data;
using Parliament_API.Models;
using System.Linq;

namespace Parliament_API.Controllers
{
    [ApiController]
    [Route("api/candidates")]
    public class CandidatesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CandidatesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET ALL CANDIDATES
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Candidates
                .Select(c => new { c.Id, c.Name, c.Party })
                .ToList());
        }

        // GET CANDIDATE BY ID
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var candidate = _context.Candidates.Find(id);
            if (candidate == null) return NotFound();

            return Ok(new { candidate.Id, candidate.Name, candidate.Party });
        }

        // ADD CANDIDATE
        [HttpPost]
        public IActionResult Add(Candidate model)
        {
            _context.Candidates.Add(model);
            _context.SaveChanges();
            return Ok("Candidate added");
        }

        // UPDATE CANDIDATE
        [HttpPut("{id}")]
        public IActionResult Update(int id, Candidate model)
        {
            var candidate = _context.Candidates.Find(id);
            if (candidate == null) return NotFound();

            candidate.Name = model.Name;
            candidate.Party = model.Party;

            _context.SaveChanges();
            return Ok("Candidate updated");
        }

        // DELETE CANDIDATE
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var candidate = _context.Candidates.Find(id);
            if (candidate == null) return NotFound();

            _context.Candidates.Remove(candidate);
            _context.SaveChanges();
            return Ok("Candidate deleted");
        }
    }
}
