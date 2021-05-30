using System.Collections.Generic;

namespace Groups.Application.TestQueries
{
    public class TestView : TestBasicView
    {
        public IEnumerable<QuestionView> Questions { get; set; } = null!;
    }

    public class QuestionView
    {
        public string Title { get; set; } = string.Empty;
        public byte[]? Photo { get; set; }
        public bool ClosedQuestion { get; init; } = true;
        public int PointsForQuestion { get; set; } = 0;
        public bool ManyCorrectAnswers { get; set; }
        public IEnumerable<string>? PossibleAnswers { get; set; }
    }
}
