using Groups.Domain.Aggregates;
using Groups.Domain.Test.Aggregates;
using Groups.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Groups.Domain.Test.ValueObjects
{
    public class StudentAnswerTest
    {
        private readonly QuestionAggregate question;

        public StudentAnswerTest()
        {
            var answers = new List<Answer>
            {
                new Answer("A", true),
                new Answer("B", true),
                new Answer("C", false),
                new Answer("D", false),
            };

            question = new QuestionAggregateBuilder()
                .WithAnswers(answers)
                .Build();
        }

        [Fact]
        public void ShouldCheckFullyCorrectAnswer()
        {
            var receivedAnswers = new List<string> { "A", "B" };
            var studentAnswer = new StudentAnswer(question, receivedAnswers);

            Assert.Equal(question.PointsForQuestion, studentAnswer.PointsForAnswer);
        }

        [Fact]
        public void ShouldCheckPartiallyCorrectAnswer()
        {
            var receivedAnswers = new List<string> { "A" };
            var studentAnswer = new StudentAnswer(question, receivedAnswers);

            Assert.Equal(2, studentAnswer.PointsForAnswer);
        }

        [Fact]
        public void ShouldCheckForWrongOrEmptyAnswer()
        {
            var receivedAnswers = new List<string> { "C" };
            var studentAnswer1 = new StudentAnswer(question, receivedAnswers);
            var studentAnswer2 = new StudentAnswer(question, new List<string>());

            Assert.Equal(0, studentAnswer1.PointsForAnswer);
            Assert.Equal(0, studentAnswer2.PointsForAnswer);
        }
    }
}
