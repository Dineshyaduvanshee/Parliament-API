using System.ComponentModel.DataAnnotations;

namespace Parliament_API.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string PasswordHash { get; set; } = null!;

        public string Role { get; set; } = "User"; // Admin/User
    }
    public class Candidate
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Party { get; set; } = null!;
    }

    public class Vote
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int CandidateId { get; set; }

        public DateTime VotedDate { get; set; } = DateTime.Now;

        // Navigation properties (optional, EF Core uses them internally)
        public User? User { get; set; }
        public Candidate? Candidate { get; set; }
    }


}
