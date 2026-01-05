using System.ComponentModel.DataAnnotations;

namespace Parliament_API.Models
{
    public class Fir
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirNumber { get; set; } = null!;   // Unique FIR No

        [Required]
        public string CrimeType { get; set; } = null!;

        public string? Description { get; set; }

        // Complainant details
        public string? ComplainantName { get; set; }
        public string? ComplainantPhone { get; set; }
        public string? ComplainantAddress { get; set; }

        // Police details
        public string? PoliceStation { get; set; }
        public string? District { get; set; }
        public string? State { get; set; }

        // Investigation
        public string? Status { get; set; }   // Registered / Under Investigation / Closed
        public DateTime FirDate { get; set; }

        // Optional link
        public int? CriminalId { get; set; }  // FK (optional)
    }
}
