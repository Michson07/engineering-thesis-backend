using Groups.Domain;
using Groups.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Groups.Application.TestResultQueries
{
    public class TestResultMapper
    {
        public TestResultView MapToTestResultView(TestResultAggregate userResult)
        {
            return new TestResultView
            {
                State = userResult.State,
                ReceivedPoints = userResult.ReceivedPoints,
                InfoAboutPoints = GetStatus(userResult),
                Questions = MapToQuestionsView(userResult)
            };
        }

        private string? GetStatus(TestResultAggregate testResult)
        {
            var testIsFinished = testResult.Test.Date.AddMinutes(testResult.Test.TimeDuration) < DateTime.Now;

            if (!testIsFinished)
            {
                return "Test się jeszcze nie skończył";
            }

            if (testResult.Checked == CheckedState.NotChecked)
            {
                return "Nie wszystkie odpowiedzi zostały sprawdzone";
            }

            return null;
        }

        private IEnumerable<QuestionView> MapToQuestionsView(TestResultAggregate testResult)
        {
            var questionsView = new List<QuestionView>();

            foreach (var question in testResult.StudentAnswers)
            {
                questionsView.Add(new QuestionView
                {
                    Question = question.Question.Title,
                    ReceivedPoints = question.PointsForAnswer,
                    PossiblePoints = question.Question.PointsForQuestion,
                    UserAnswers = question.ReceivedAnswers,
                    CorrectAnswers = question.Question.Answers?.Where(a => a.Correct).Select(a => a.Value)
                });
            }

            return questionsView;
        }
    }
}
