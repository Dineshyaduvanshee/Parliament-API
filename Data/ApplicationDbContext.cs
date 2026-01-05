using Microsoft.EntityFrameworkCore;
using Parliament_API.Models;

namespace Parliament_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<MpProfile> MpProfiles { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<ReporterProfile> ReporterProfile { get; set; }
        public DbSet<ParliamentDebateTopic> ParliamentDebateTopics { get; set; }
        public DbSet<PoliceOfficer> PoliceOfficers { get; set; }
        public DbSet<CriminalProfile> CriminalProfiles { get; set; }
        public DbSet<Fir> Firs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Payment> Payments { get; set; }


    }
}
