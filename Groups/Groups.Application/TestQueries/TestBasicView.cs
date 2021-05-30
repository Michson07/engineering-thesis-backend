using System;

namespace Groups.Application.TestQueries
{
    public class TestBasicView
    {
        public string Name { get; set; } = string.Empty;
        public bool RequirePhoto { get; set; } = false;
        public int? PassedFrom { get; set; }
        public int TimeDuration { get; set; }
        public DateTime Date { get; set; }
    }
}
