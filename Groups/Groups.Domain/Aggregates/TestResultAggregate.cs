using Core.Domain;
using FluentValidation;
using Groups.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Groups.Domain.Aggregates
{
    public class TestResultAggregate : AggregateRoot
    {
        public TestAggregate Test { get; private set; } = null!;
        public Participient Student { get; private set; } = null!;
        public IEnumerable<StudentAnswer> StudentAnswers { get; private set; } = null!;
        public string? State { get; private set; } = null;
        public int ReceivedPoints => StudentAnswers.Sum(sa => sa.PointsForAnswer);

        private TestResultAggregate()
        {
        }

        private TestResultAggregate(TestAggregate test, Participient student, 
            IEnumerable<StudentAnswer> studentAnswers) : base()
        {
            Test = test;
            Student = student;
            StudentAnswers = studentAnswers;
            if(test.PassedFrom != null)
            {
                var resultInPercents = ReceivedPoints / test.MaxPoints * 100;
                State = resultInPercents >= test.PassedFrom ? "Passed" : "NotPassed";
            }
        }

        public static TestResultAggregate Create(TestAggregate test, Participient student,
            IEnumerable<StudentAnswer> studentAnswers)
        {
            var testResult = new TestResultAggregate(test, student, studentAnswers);
            new TestResultValidation().ValidateAndThrow(testResult);
            if(testResult.ReceivedPoints > test.MaxPoints)
            {
                throw new ApplicationException("Received points cannot be greater than max test points");
            }

            return testResult;
        }
    }

    public class TestResultValidation : AbstractValidator<TestResultAggregate>
    {
        public TestResultValidation()
        {
            RuleFor(tr => tr.Id).NotEmpty();
            RuleFor(tr => tr.Test).NotNull();
            RuleFor(tr => tr.Student).NotNull();
            RuleFor(tr => tr.ReceivedPoints).GreaterThan(0);
            When(tr => tr.State != null, () =>
            {
                RuleFor(tr => tr.State).Must(state => state == "Passed" || state == "NotPassed");
            });
        }
    }
}
