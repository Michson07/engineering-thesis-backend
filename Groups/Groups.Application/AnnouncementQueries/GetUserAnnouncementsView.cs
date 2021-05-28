using System;

namespace Groups.Application.AnnouncementQueries
{
    public class GetUserAnnouncementsView
    {
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}
