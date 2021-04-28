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
        public DateTime Date { get; set; } = default!;
        public bool RequirePhoto { get; set; } = false;
        public int? PassedFrom { get; set; } //must be in %

        public int MaxPoints => Questions.Sum(q => q.PointsForQuestion);

        private TestAggregate()
        {
        }

        private TestAggregate(string name, IEnumerable<QuestionAggregate> questions,
            GroupAggregate group, DateTime date, bool requirePhoto, int? passedFrom) : base()
        {
            Name = name;
            Questions = questions;
            Group = group;
            Date = date;
            RequirePhoto = requirePhoto;
            PassedFrom = passedFrom;
        }

        public static TestAggregate Create(string name, IEnumerable<QuestionAggregate> questions,
            GroupAggregate group, DateTime date, bool requirePhoto = false, int? passedFrom = null)
        {
            var test = new TestAggregate(name, questions, group, date, requirePhoto, passedFrom);
            new TestAggregateValidation().ValidateAndThrow(test);

            return test;
        }

        public TestAggregate Update(string name, IEnumerable<QuestionAggregate> questions, GroupAggregate group, bool requirePhoto, int? passedFrom)
        {
            Name = name;
            Questions = questions;
            Group = group;
            RequirePhoto = requirePhoto;
            PassedFrom = passedFrom;
            new TestAggregateValidation().ValidateAndThrow(this);

            return this;
        }
    }

    public class TestAggregateValidation : AbstractValidator<TestAggregate>
    {
        public TestAggregateValidation()
        {
            RuleFor(test => test.Id).NotEmpty();
            RuleFor(test => test.Name).NotEmpty().MaximumLength(50);
            RuleFor(test => test.Questions).NotEmpty().NotNull();
            RuleFor(test => test.Group).NotNull();
            RuleFor(test => test.MaxPoints).NotEmpty().GreaterThan(0);
            When(test => test.PassedFrom != null, () =>
            {
                RuleFor(test => test.PassedFrom).ExclusiveBetween(0, 100);
            });
        }
    }
}
