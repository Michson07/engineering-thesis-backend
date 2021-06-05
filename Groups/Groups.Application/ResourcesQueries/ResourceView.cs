using System;

namespace Groups.Application.ResourcesQueries
{
    public class ResourceView
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }
}
