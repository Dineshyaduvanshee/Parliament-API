using System.ComponentModel.DataAnnotations;

namespace Parliament_API.Models
{
    public class ReporterProfile
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Media House is required")]
        [StringLength(100)]
        public string MediaHouse { get; set; } = string.Empty;

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

        [StringLength(20)]
        public string? Phone { get; set; }

        // ⚠️ URL validation removed (frontend already validates)
        [StringLength(300)]
        public string? Website { get; set; }

      
        public string? PhotoUrl { get; set; }


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
