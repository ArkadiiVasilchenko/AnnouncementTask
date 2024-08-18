using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Announcement.Domain.Models;

namespace Announcement.Persistence.Configurations
{
    public class AnnouncementConfiguration : IEntityTypeConfiguration<AnnouncementEntity>
    {
        public void Configure(EntityTypeBuilder<AnnouncementEntity> builder)
        {
            builder.HasKey(u => u.Id);
        }
    }
}
