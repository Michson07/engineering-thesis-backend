using System.Collections.Generic;

namespace Groups.Application.TestQueries
{
    public class TestView
    {
        public string Name { get; set; } = string.Empty;
        public IEnumerable<QuestionView> Questions { get; set; } = null!;
        public bool RequirePhoto { get; set; } = false;
        public int? PassedFrom { get; set; }
    }

    public class QuestionView
    {
        public string Title { get; set; } = string.Empty;
        public byte[]? Photo { get; set; }
        public bool ClosedQuestion { get; init; } = true;
        public int PointsForQuestion { get; set; } = 0;
        public bool ManyCorrectAnswers { get; set; }
    }
}
