using Core.Domain;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Groups.Domain.Aggregates
{
    public class TestAggregate : AggregateRoot
    {
        public string Name { get; private set; } = null!;
        public IEnumerable<QuestionAggregate> Questions { get; private set; } = null!;
        public GroupAggregate Group { get; private set; } = null!;
        public DateTime Date { get; private set; } = default!;
        public bool RequirePhoto { get; private set; } = false;
        public int TimeDuration { get; private set; } //must be in minutes
        public int? PassedFrom { get; private set; } //must be in %

        public int MaxPoints => Questions.Sum(q => q.PointsForQuestion);

        private TestAggregate()
        {
        }

        private TestAggregate(string name, IEnumerable<QuestionAggregate> questions,
            GroupAggregate group, DateTime date, bool requirePhoto, int? passedFrom, int timeDuration) : base()
        {
            Name = name;
            Questions = questions;
            Group = group;
            Date = date;
            RequirePhoto = requirePhoto;
            PassedFrom = passedFrom;
            TimeDuration = timeDuration;
        }

        public static TestAggregate Create(string name, IEnumerable<QuestionAggregate> questions,
            GroupAggregate group, DateTime date, bool requirePhoto = false, int? passedFrom = null, int timeDuration = 60)
        {
            var test = new TestAggregate(name, questions, group, date, requirePhoto, passedFrom, timeDuration);
            new TestAggregateValidation().ValidateAndThrow(test);

            return test;
        }

        public TestAggregate Update(string name, IEnumerable<QuestionAggregate> questions, GroupAggregate group, DateTime date, bool requirePhoto, int? passedFrom, int timeDuration)
        {
            var test = new TestAggregate(name, questions, group, date, requirePhoto, passedFrom, timeDuration);
            new TestAggregateValidation().ValidateAndThrow(test);

            Name = name;
            Questions = questions;
            Group = group;
            Date = date;
            RequirePhoto = requirePhoto;
            PassedFrom = passedFrom;
            TimeDuration = timeDuration;

            return this;
        }
    }

    public class TestAggregateValidation : AbstractValidator<TestAggregate>
    {
        public TestAggregateValidation()
        {
            RuleFor(test => test.Id).NotEmpty();
            RuleFor(test => test.Name).NotEmpty().MaximumLength(50);
            RuleFor(test => test.Questions).NotEmpty();
            RuleFor(test => test.MaxPoints).NotEmpty().GreaterThan(0);
            RuleFor(test => test.TimeDuration).GreaterThan(0);
            When(test => test.PassedFrom != null, () =>
            {
                RuleFor(test => test.PassedFrom).ExclusiveBetween(0, 100);
            });
        }
    }
}
