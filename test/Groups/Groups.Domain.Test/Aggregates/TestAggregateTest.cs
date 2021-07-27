using FluentValidation;
using Groups.Domain.Aggregates;
using Groups.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Groups.Domain.Test.Aggregates
{
    public class TestAggregateTest
    {
        [Fact]
        public void ShouldCreateTest()
        {
            var test = new TestAggregateBuilder().Build();

            Assert.Equal("Test 1", test.Name);
            Assert.True(test.Questions.Count() == 3);
            Assert.NotNull(test.Group);
            Assert.Equal(new DateTime(2021, 10, 15, 15, 0, 0), test.Date);
            Assert.False(test.RequirePhoto);
            Assert.True(test.TimeDuration == 45);
            Assert.True(test.PassedFrom == 50);
            Assert.True(test.MaxPoints == 8);
        }

        [Fact]
        public void ShouldUpdateTest()
        {
            var test = new TestAggregateBuilder().Build();
            test.Update(
                "New name",
                new List<QuestionAggregate>() { new QuestionAggregateBuilder().WithTitle("New Question").WithAnswers(new List<Answer> { new Answer("Correct", true), new Answer("Wrong", false) }).Build() },
                test.Group,
                new DateTime(2021, 12, 20, 17, 30, 0),
                true,
                null,
                60
            );

            Assert.Equal("New name", test.Name);
            Assert.True(test.Questions.Count() == 1);
            Assert.NotNull(test.Group);
            Assert.Equal(new DateTime(2021, 12, 20, 17, 30, 0), test.Date);
            Assert.True(test.RequirePhoto);
            Assert.True(test.TimeDuration == 60);
            Assert.Null(test.PassedFrom);
            Assert.True(test.MaxPoints == 2);
        }

        [Fact]
        public void ShouldNotAllowToHaveNameLengthMoreThan50Chars()
        {
            Assert.Throws<ValidationException>(() =>
                new TestAggregateBuilder()
                .WithName(CreateLongText())
                .Build()
            );
        }

        [Fact]
        public void ShouldNotAllowToCreateWithoutQuestions()
        {
            Assert.Throws<ValidationException>(() =>
                new TestAggregateBuilder()
                .WithQuestions(new List<QuestionAggregate>())
                .Build()
            );
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        public void ShouldNotAllowToCreateWithMaxPointsLessOrEqualTo0(int points)
        {
            Assert.Throws<ValidationException>(() =>
                new TestAggregateBuilder()
                .WithQuestions(new List<QuestionAggregate>() { new QuestionAggregateBuilder().WithPointsForAnswer(points).Build() })
                .Build()
            );
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        public void ShouldNotAllowToCreateWithTimeDurationLessOrEqualTo0(int timeDuration)
        {
            Assert.Throws<ValidationException>(() =>
                new TestAggregateBuilder()
                .WithTimeDuration(timeDuration)
                .Build()
            );
        }

        [Theory]
        [InlineData(101)]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        [InlineData(int.MaxValue)]
        public void ShouldNotAllowToCreateWithPassedFromOtherThanBetween0And100(int passedFrom)
        {
            Assert.Throws<ValidationException>(() =>
                new TestAggregateBuilder()
                .WithPassedFrom(passedFrom)
                .Build()
            );
        }

        private string CreateLongText()
        {
            var text = new StringBuilder();
            for(var i = 0; i < 51; i++)
            {
                text.Append('a');
            }

            return text.ToString();
        }
    }
}
