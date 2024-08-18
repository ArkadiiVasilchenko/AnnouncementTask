namespace Announcement.Domain.Models.RequestDtos
{
    public class CreateAnnouncementRequestDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
