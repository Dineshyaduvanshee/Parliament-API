using System;
using System.ComponentModel.DataAnnotations;

namespace Parliament_API.Models
{
    public class HelpSupport
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(150)]
        public string Email { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Message { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "Open";

        // 🔹 Photo URL (Cloudinary / Firebase / S3 / local)
        [MaxLength(500)]
        public string? PhotoUrl { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
