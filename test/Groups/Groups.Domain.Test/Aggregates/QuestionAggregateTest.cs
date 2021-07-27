using Core.Domain.ValueObjects;
using FluentValidation;
using Groups.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Groups.Domain.Test.Aggregates
{
    public class QuestionAggregateTest
    {
        [Fact]
        public void ShouldCreateQuestion()
        {
            var photo = new Photo(new byte[] { 0, 1, 2, 3 });
            var question = new QuestionAggregateBuilder()
                .WithPhoto(photo)
                .WithClosedQuestion(true)
                .WithAnswers(new List<Answer>() { new Answer("Correct", true), new Answer("Wrong", false) })
                .Build();

            Assert.Equal("Question 1", question.Title);
            Assert.Equal(photo, question.Photo);
            Assert.NotNull(question.Answers);
            Assert.True(question.Answers!.Count() == 2);
            Assert.True(question.ClosedQuestion);
            Assert.True(question.PointsForQuestion == 2);
            Assert.False(question.ManyCorrectAnswers);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        [InlineData(0)]
        public void ShouldNotAllowToHavePointsForQuestionNegativeOrZero(int points)
        {
            Assert.Throws<ValidationException>(() => new QuestionAggregateBuilder()
                .WithPointsForAnswer(points)
                .WithAnswers(new List<Answer>() { new Answer("Correct", true), new Answer("Wrong", false) })
                .Build());
        }

        [Fact]
        public void ShouldNotAllowToHaveEmptyTitle()
        {
            Assert.Throws<ValidationException>(() => new QuestionAggregateBuilder()
                .WithTitle(string.Empty)
                .Build());
        }

        [Fact]
        public void ShouldNotAllowToNotHaveCorrectAnswerWhenHaveAnswers()
        {
            Assert.Throws<ValidationException>(() => new QuestionAggregateBuilder()
                .WithAnswers(new List<Answer>() { new Answer("Wrong1", false), new Answer("Wrong2", false) })
                .Build());
        }

        [Fact]
        public void ShouldNotAllowToNotHaveLessThan2AnswersWhenItsClosedQuestion()
        {
            Assert.Throws<ValidationException>(() => new QuestionAggregateBuilder()
                .WithClosedQuestion(true)
                .WithAnswers(new List<Answer>() { new Answer("Answer", true) })
                .Build());
        }
    }
}
