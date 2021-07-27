using Core.Domain.Test;
using Core.Domain.ValueObjects;
using Groups.Domain.Aggregates;
using Groups.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace Groups.Domain.Test.Aggregates
{
    public class TestAggregateBuilder : Builder<TestAggregate>
    {
        private string name = "Test 1";
        private IEnumerable<QuestionAggregate> questions = new List<QuestionAggregate>()
        {
            new QuestionAggregateBuilder().WithTitle("Question 1").WithAnswers(new List<Answer> { new Answer("Correct", true), new Answer("Wrong", false) }).Build(),
            new QuestionAggregateBuilder().WithTitle("Question 2").WithPointsForAnswer(1).WithClosedQuestion(false).WithAnswers(new List<Answer> { new Answer("10", true) }).Build(),
            new QuestionAggregateBuilder().WithTitle("Question 3").WithPointsForAnswer(5).WithClosedQuestion(false).WithPhoto(new Photo(new byte[] { 1, 2, 3 })).Build()
        };

        private GroupAggregate group = new GroupAggregateBuilder().Build();
        private DateTime date = new DateTime(2021, 10, 15, 15, 0, 0);
        private bool requirePhoto = false;
        private int timeDuration = 45;
        private int? passedFrom = 50;

        protected override TestAggregate BuildInstance()
        {
            return TestAggregate.Create(name, questions, group, date, requirePhoto, passedFrom, timeDuration);
        }

        public TestAggregateBuilder WithName(string name)
        {
            this.name = name;

            return this;
        }

        public TestAggregateBuilder WithQuestions(IEnumerable<QuestionAggregate> questions)
        {
            this.questions = questions;

            return this;
        }

        public TestAggregateBuilder WithGroup(GroupAggregate group)
        {
            this.group = group;

            return this;
        }

        public TestAggregateBuilder WithDate(DateTime date)
        {
            this.date = date;

            return this;
        }

        public TestAggregateBuilder WithRequirePhoto(bool requirePhoto)
        {
            this.requirePhoto = requirePhoto;

            return this;
        }

        public TestAggregateBuilder WithTimeDuration(int timeDuration)
        {
            this.timeDuration = timeDuration;

            return this;
        }

        public TestAggregateBuilder WithPassedFrom(int? passedFrom)
        {
            this.passedFrom = passedFrom;

            return this;
        }
    }
}
