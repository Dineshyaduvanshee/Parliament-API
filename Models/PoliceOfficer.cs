using System.ComponentModel.DataAnnotations;

namespace Parliament_API.Models
{
    public class PoliceOfficer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        public string? Rank { get; set; }           // Inspector, SI, Constable
        public string? BadgeNumber { get; set; }

        public string? PoliceStation { get; set; }
        public string? District { get; set; }
        public string? State { get; set; }

        public string? Department { get; set; }     // Crime, Traffic, Cyber
        public int? ExperienceYears { get; set; }

        public string? Phone { get; set; }
        public string? Email { get; set; }

        public string? Status { get; set; }          // Active / Suspended / Retired
    }
}
