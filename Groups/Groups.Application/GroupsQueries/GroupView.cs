using System;
using System.Collections.Generic;

namespace Groups.Application.GroupsQueries
{
    public class GroupView
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IEnumerable<TestGroupView>? Tests { get; set; }
    }

    public class TestGroupView
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public DateTime Date { get; set; } = default!;
    }
}
