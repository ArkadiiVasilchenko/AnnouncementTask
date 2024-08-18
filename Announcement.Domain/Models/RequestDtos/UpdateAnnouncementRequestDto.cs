using System;
namespace Announcement.Domain.Models.RequestDtos
{
    public class UpdateAnnouncementRequestDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; }  = string.Empty;
    }
}
