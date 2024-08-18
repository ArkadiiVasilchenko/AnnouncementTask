using Announcement.Domain.Models;
using Announcement.Domain.Models.RequestDtos;

namespace Announcement.Application.Services.AnnouncementServices.AnnouncementServicesInterfaces
{
    public interface IAnnouncementService : IBaseService<AnnouncementEntity>
    {
        Task СreateAsync(CreateAnnouncementRequestDto requestDto);
        Task UpdateAsync(UpdateAnnouncementRequestDto requestDto);
        Task<List<AnnouncementEntity>> ReadByIdAsync(int id);
    }
}
