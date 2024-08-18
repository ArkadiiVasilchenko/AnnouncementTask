namespace Announcement.Domain.Models
{
    public class AnnouncementEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DateTimeCreation { get; set; } = DateTime.Now;

        public AnnouncementEntity(string title, string description)
        {
            Title = title;
            Description = description;
        }
    }
}
