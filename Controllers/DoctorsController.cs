using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parliament_API.Data;
using Parliament_API.Models;

namespace Parliament_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DoctorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ GET: api/Doctors
        [HttpGet]
        public IActionResult GetDoctors()
        {
            var doctors = _context.Doctors.ToList();
            return Ok(doctors);
        }

        // ✅ GET: api/Doctors/5
        [HttpGet("{id:int}")]
        public IActionResult GetDoctorById(int id)
        {
            var doctor = _context.Doctors.Find(id);
            if (doctor == null)
                return NotFound(new { message = "Doctor not found" });

            return Ok(doctor);
        }

        // ✅ POST: api/Doctors
        [HttpPost]
        public IActionResult AddDoctor([FromBody] Doctor doctor)
        {
            if (doctor == null || string.IsNullOrEmpty(doctor.Name) || string.IsNullOrEmpty(doctor.Specialization) || string.IsNullOrEmpty(doctor.Phone))
                return BadRequest(new { message = "Invalid doctor data" });

            _context.Doctors.Add(doctor);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetDoctorById), new { id = doctor.Id }, doctor);
        }

        // ✅ PUT: api/Doctors/5
        [HttpPut("{id:int}")]
        public IActionResult UpdateDoctor(int id, [FromBody] Doctor doctor)
        {
            var existing = _context.Doctors.Find(id);
            if (existing == null)
                return NotFound(new { message = "Doctor not found" });

            existing.Name = doctor.Name;
            existing.Specialization = doctor.Specialization;
            existing.Phone = doctor.Phone;

            _context.SaveChanges();
            return Ok(existing);
        }

        // ✅ DELETE: api/Doctors/5
        [HttpDelete("{id:int}")]
        public IActionResult DeleteDoctor(int id)
        {
            var doctor = _context.Doctors.Find(id);
            if (doctor == null)
                return NotFound(new { message = "Doctor not found" });

            _context.Doctors.Remove(doctor);
            _context.SaveChanges();

            return Ok(new { message = "Doctor deleted successfully" });
        }
    }
}
