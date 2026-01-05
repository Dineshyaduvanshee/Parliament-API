using System.ComponentModel.DataAnnotations;

namespace Parliament_API.Models
{
    public class ReporterProfile
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string MediaHouse { get; set; } = null!;

        [StringLength(100)]
        public string? Beat { get; set; }

        [StringLength(100)]
        public string? City { get; set; }

        [StringLength(100)]
        public string? State { get; set; }

        [Range(0, 60)]
        public int? ExperienceYears { get; set; }

        [EmailAddress]
        [StringLength(150)]
        public string? Email { get; set; }

        [Phone]
        [StringLength(20)]
        public string? Phone { get; set; }

        [Url]
        [StringLength(200)]
        public string? Website { get; set; }

        [StringLength(300)]
        public string? PhotoUrl { get; set; }

        // 🔹 Optional Future Fields
        [StringLength(100)]
        public string? Designation { get; set; }

        [StringLength(200)]
        public string? Education { get; set; }

        [StringLength(100)]
        public string? TwitterHandle { get; set; }

        [StringLength(100)]
        public string? InstagramHandle { get; set; }
    }
}
