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
        public string Checked { get; private set; } = CheckedState.NotChecked;
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
            if(test.Questions.All(q => q.Answers != null))
            {
                Checked = CheckedState.Checked;
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

        public static TestResultAggregate CreateForAbsent(TestAggregate test, Participient student)
        {
            var studentAnswers = new List<StudentAnswer>();
            foreach(var question in test.Questions)
            {
                studentAnswers.Add(new StudentAnswer(question, new List<string> { "" }, 0));
            }

            var testResult = new TestResultAggregate(test, student, studentAnswers);
            new TestResultValidation().ValidateAndThrow(testResult);
            if (testResult.ReceivedPoints > test.MaxPoints)
            {
                throw new ApplicationException("Received points cannot be greater than max test points");
            }

            return testResult;
        }

        public TestResultAggregate Update(TestAggregate test, Participient student,
            IEnumerable<StudentAnswer> studentAnswers)
        {
            var testResult = new TestResultAggregate(test, student, studentAnswers);
            new TestResultValidation().ValidateAndThrow(testResult);

            Test = test;
            Student = student;
            StudentAnswers = studentAnswers;

            if (test.PassedFrom != null)
            {
                var resultInPercents = ReceivedPoints / test.MaxPoints * 100;
                State = resultInPercents >= test.PassedFrom ? "Passed" : "NotPassed";
            }

            Checked = CheckedState.Checked;
            
            if (ReceivedPoints > test.MaxPoints)
            {
                throw new ApplicationException("Received points cannot be greater than max test points");
            }

            return this;
        }
    }

    public class TestResultValidation : AbstractValidator<TestResultAggregate>
    {
        public TestResultValidation()
        {
            RuleFor(tr => tr.Id).NotEmpty();
            RuleFor(tr => tr.ReceivedPoints).GreaterThanOrEqualTo(0);
            RuleFor(tr => tr.Checked).Must(state => state == CheckedState.NotChecked || state == CheckedState.Checked);
            When(tr => tr.State != null, () =>
            {
                RuleFor(tr => tr.State).Must(state => state == "Passed" || state == "NotPassed");
            });
        }
    }
}
