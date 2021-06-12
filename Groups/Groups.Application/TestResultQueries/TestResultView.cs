using System.Collections.Generic;

namespace Groups.Application.TestResultQueries
{
    public class TestResultView
    {
        public string? State { get; set; }
        public bool Checked { get; set; }
        public int ReceivedPoints { get; set; }
        public string? InfoAboutPoints { get; set; }
        public string TestName { get; set; } = string.Empty;
        public int? PassedFrom { get; set; }
        public IEnumerable<QuestionView> Questions { get; set; } = new List<QuestionView>();
    }

    public class QuestionView
    {
        public string Question { get; set; } = string.Empty;
        public int ReceivedPoints { get; set; } = 0;
        public int PossiblePoints { get; set; } = 0;
        public IEnumerable<string>? UserAnswers { get; set; }
        public IEnumerable<string>? CorrectAnswers { get; set; }
    }
}
