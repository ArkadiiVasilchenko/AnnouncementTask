using Announcement.Domain.Models;
using Announcement.Domain.Models.RequestDtos;
using Announcement.Persistence.Data;

namespace Announcement.Persistence.Repositories.AnnouncementRepository
{
    public class AnnouncementRepository : BaseRepository<AnnouncementEntity>, IAnnouncementRepository
    {
        private readonly AppDbContext dbContext;
        public AnnouncementRepository(AppDbContext _dbContext) : base(_dbContext)
        {
            dbContext = _dbContext;
        }
    }
}
