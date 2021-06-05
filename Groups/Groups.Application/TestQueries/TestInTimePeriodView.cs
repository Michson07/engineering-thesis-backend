using System;
using System.Collections.Generic;

namespace Groups.Application.TestQueries
{
    public class TestInTimePeriodView
    {
        public string TestId { get; set; } = string.Empty;
        public string TestName { get; set; } = string.Empty;
        public int TestTimeDuration { get; set; }
        public DateTime TestDate { get; set; }
        public bool TestRequirePhoto { get; set; }
        public IEnumerable<string> Emails { get; set; } = new List<string>();
    }
}
