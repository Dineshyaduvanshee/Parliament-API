using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parliament_API.Data;
using Parliament_API.Models;


namespace Parliament_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MembersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/members
        [HttpGet]
        public async Task<IActionResult> GetAllMembers()
        {
            var members = await _context.MpProfiles.ToListAsync();
            return Ok(members);
        }

        // GET: api/members/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMemberById(int id)
        {
            var member = await _context.MpProfiles.FindAsync(id);
            if (member == null)
                return NotFound();

            return Ok(member);
        }

        // POST: api/members
        [HttpPost]
        public async Task<IActionResult> CreateMember([FromBody] MpProfile member)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _context.MpProfiles.AddAsync(member);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMemberById), new { id = member.Id }, member);
        }

        // PUT: api/members/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMember(int id, [FromBody] MpProfile updatedMember)
        {
            if (id != updatedMember.Id)
                return BadRequest("ID mismatch");

            var member = await _context.MpProfiles.FindAsync(id);
            if (member == null)
                return NotFound();

            // Update fields
            member.Name = updatedMember.Name;
            member.House = updatedMember.House;
            member.Constituency = updatedMember.Constituency;
            member.State = updatedMember.State;
            member.Party = updatedMember.Party;
            member.Role = updatedMember.Role;
            member.Education = updatedMember.Education;
            member.TermStart = updatedMember.TermStart;
            member.Email = updatedMember.Email;
            member.Website = updatedMember.Website;
            member.PhotoUrl = updatedMember.PhotoUrl;

            _context.MpProfiles.Update(member);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/members/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            var member = await _context.MpProfiles.FindAsync(id);
            if (member == null)
                return NotFound();

            _context.MpProfiles.Remove(member);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
