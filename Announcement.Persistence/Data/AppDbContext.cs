using Announcement.Domain.Models;
using Announcement.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Announcement.Persistence.Data
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration configuration;
        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration _configuration) : base(options)
        {
            configuration = _configuration;
        }

        public DbSet<AnnouncementEntity> Announcements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AnnouncementConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            base.OnConfiguring(optionsBuilder);
        }
    }
}
