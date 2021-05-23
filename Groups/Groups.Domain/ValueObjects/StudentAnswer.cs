using Core.Domain.ValueObjects;
using Groups.Domain.Aggregates;
using System.Collections.Generic;
using System.Linq;

namespace Groups.Domain.ValueObjects
{
    public class StudentAnswer : ValueObject
    {
        public QuestionAggregate Question { get; init; } = null!;
        public IEnumerable<string> ReceivedAnswers { get; init; } = null!;
        public int PointsForAnswer { get; private set; } = 0;

        private StudentAnswer()
        {
        }

        public StudentAnswer(QuestionAggregate question, IEnumerable<string> receivedAnswers)
        {
            Question = question;
            ReceivedAnswers = receivedAnswers;
            PointsForAnswer = CheckAnswers(question);
        }

        private int CheckAnswers(QuestionAggregate question)
        {
            var points = 0;
            if (question.Answers != null)
            {
                var partialPoints = question.PointsForQuestion / question.Answers.Count(a => a.Correct);
                foreach (var answer in ReceivedAnswers)
                {
                    if(question.Answers.Where(a => a.Correct).Select(a => a.Value.ToLower().Trim()).Contains(answer.ToLower().Trim()))
                    {
                        points += partialPoints;
                    }
                }
            }

            return points;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Question;
            yield return ReceivedAnswers;
            yield return PointsForAnswer;
        }
    }
}
