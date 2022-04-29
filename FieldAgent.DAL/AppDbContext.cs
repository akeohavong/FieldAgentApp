
using FieldAgent.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace FieldAgent.DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<Agency> Agency { get; set; }
        public DbSet<AgencyAgent> AgencyAgent { get; set; }

        public DbSet<Agent> Agent { get; set; }

        public DbSet<Alias> Alias { get; set; }

        public DbSet<Location> Location { get; set; }
        public DbSet<Mission> Mission { get; set; }
        public DbSet<MissionAgent> MissionAgent { get; set; }
        public DbSet<SecurityClearance> SecurityClearance { get; set; }

        public AppDbContext() : base()
        {

        }

        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AgencyAgent>()
                .HasKey(aa => new { aa.AgencyID, aa.AgentID });

            builder.Entity<MissionAgent>()
                .HasKey(aa => new { aa.MissionID, aa.AgentID });
           
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          
            optionsBuilder.LogTo(message => Debug.WriteLine(message), LogLevel.Information);
        }
    }
}
