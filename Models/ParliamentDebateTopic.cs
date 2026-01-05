using System.ComponentModel.DataAnnotations;

namespace Parliament_API.Models
{
    public class ParliamentDebateTopic
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string TopicName { get; set; } = null!;
        // e.g. Education, Law & Order, Economy

        public string? Description { get; set; }

        public string? Ministry { get; set; }
        // Education Ministry, Home Ministry, Finance Ministry

        public string? House { get; set; }
        // Lok Sabha / Rajya Sabha

        public string? Status { get; set; }
        // Ongoing / Completed / Scheduled

        public DateTime? DebateDate { get; set; }

        public string? IntroducedBy { get; set; }
        // MP / Minister name
    }
}
