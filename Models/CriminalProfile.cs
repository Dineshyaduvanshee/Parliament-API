using System.ComponentModel.DataAnnotations;

namespace Parliament_API.Models
{
    public class CriminalProfile
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; } = null!;

        public string? Alias { get; set; }          // Nickname
        public int? Age { get; set; }
        public string? Gender { get; set; }

        public string? CrimeType { get; set; }      // Theft, Murder, Cyber Crime
        public string? FIRNumber { get; set; }

        public string? PoliceStation { get; set; }
        public string? District { get; set; }
        public string? State { get; set; }

        public string? Status { get; set; }          // Wanted / Arrested / Released
        public DateTime? ArrestDate { get; set; }

        public string? IdentificationMark { get; set; }
        public string? PhotoUrl { get; set; }
    }
}
