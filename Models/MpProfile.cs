using System.ComponentModel.DataAnnotations;

namespace Parliament_API.Models
{
    public class MpProfile
    {
        [Key]
        public int Id { get; set; }

        public required string Name { get; set; }
        public required string House { get; set; }
        public string? Constituency { get; set; }
        public string? State { get; set; }
        public string? Party { get; set; }
        public string? Role { get; set; }
        public string? Education { get; set; }
        public string? TermStart { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
        public string? PhotoUrl { get; set; }
    }
}
