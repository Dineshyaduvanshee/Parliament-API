using Microsoft.AspNetCore.Mvc;
using Parliament_API.Data;
using Parliament_API.Models;

namespace Parliament_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AppointmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ GET ALL
        [HttpGet]
        public IActionResult GetAppointments()
        {
            return Ok(_context.Appointments.ToList());
        }

        // ✅ GET BY ID
        [HttpGet("{id}")]
        public IActionResult GetAppointment(int id)
        {
            var appointment = _context.Appointments.Find(id);
            if (appointment == null)
                return NotFound();

            return Ok(appointment);
        }

        // ✅ CREATE
        [HttpPost]
        public IActionResult BookAppointment(Appointment appointment)
        {
            appointment.Status = appointment.Status ?? "Booked";
            _context.Appointments.Add(appointment);
            _context.SaveChanges();
            return Ok(appointment);
        }

        // ✅ UPDATE (THIS FIXES YOUR ERROR)
        [HttpPut("{id}")]
        public IActionResult UpdateAppointment(int id, Appointment appointment)
        {
            if (id != appointment.Id)
                return BadRequest("ID mismatch");

            var existing = _context.Appointments.Find(id);
            if (existing == null)
                return NotFound();

            existing.PatientId = appointment.PatientId;
            existing.DoctorId = appointment.DoctorId;
            existing.AppointmentDate = appointment.AppointmentDate;
            existing.Status = appointment.Status;

            _context.SaveChanges();
            return Ok(existing);
        }

        // ✅ DELETE
        [HttpDelete("{id}")]
        public IActionResult DeleteAppointment(int id)
        {
            var appointment = _context.Appointments.Find(id);
            if (appointment == null)
                return NotFound();

            _context.Appointments.Remove(appointment);
            _context.SaveChanges();
            return Ok();
        }
    }
}
