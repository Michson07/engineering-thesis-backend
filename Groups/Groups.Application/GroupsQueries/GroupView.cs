﻿using System;
using System.Collections.Generic;

namespace Groups.Application.GroupsQueries
{
    public class GroupView
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Code { get; set; }
        public IEnumerable<TestGroupView>? Tests { get; set; }
        public bool IsOwner { get; set; } = false;
    }

    public class TestGroupView
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public DateTime Date { get; set; } = default!;
    }
}
