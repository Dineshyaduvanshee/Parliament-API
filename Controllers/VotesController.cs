using Microsoft.AspNetCore.Mvc;
using Parliament_API.Data;
using Parliament_API.Models;
using System.Linq;

namespace Parliament_API.Controllers
{
    [ApiController]
    [Route("api/votes")]
    public class VotesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VotesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult CastVote([FromBody] Vote model)
        {
            // Prevent double voting
            if (_context.Votes.Any(v => v.UserId == model.UserId))
                return BadRequest("User has already voted");

            // Validate User exists
            if (!_context.Users.Any(u => u.Id == model.UserId))
                return BadRequest("User does not exist");

            // Validate Candidate exists
            if (!_context.Candidates.Any(c => c.Id == model.CandidateId))
                return BadRequest("Candidate does not exist");

            // Only store foreign keys, not full objects
            var vote = new Vote
            {
                UserId = model.UserId,
                CandidateId = model.CandidateId,
                VotedDate = DateTime.Now
            };

            _context.Votes.Add(vote);
            _context.SaveChanges();

            return Ok(new
            {
                message = "Vote cast successfully",
                voteId = vote.Id
            });
        }


        // GET VOTING RESULTS
        [HttpGet("results")]
        public IActionResult Results()
        {
            var results = _context.Votes
                .GroupBy(v => v.CandidateId)
                .Select(g => new
                {
                    CandidateId = g.Key,
                    CandidateName = _context.Candidates.FirstOrDefault(c => c.Id == g.Key)!.Name,
                    Votes = g.Count()
                })
                .ToList();

            return Ok(results);
        }
    }
}
