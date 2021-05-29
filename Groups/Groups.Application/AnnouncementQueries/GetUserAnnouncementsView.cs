using System;

namespace Groups.Application.AnnouncementQueries
{
    public class GetUserAnnouncementsView
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public bool IsCreator { get; set; }
    }
}
