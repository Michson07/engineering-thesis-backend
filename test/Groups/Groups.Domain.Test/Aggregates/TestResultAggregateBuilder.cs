using Core.Domain.Test;
using Core.Domain.ValueObjects;
using Groups.Domain.Aggregates;
using Groups.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace Groups.Domain.Test.Aggregates
{
    public class TestResultAggregateBuilder : Builder<TestResultAggregate>
    {
        private TestAggregate test = new TestAggregateBuilder().Build();
        private Participient student = new Participient(new Email("student@student.pl"), GroupRoles.Student);

        private IEnumerable<QuestionAggregate> questions = new List<QuestionAggregate>()
        {
           new QuestionAggregateBuilder().WithTitle("Question 1").WithAnswers(new List<Answer> { new Answer("Correct", true), new Answer("Wrong", false) }).Build(),
            new QuestionAggregateBuilder().WithTitle("Question 2").WithPointsForAnswer(1).WithClosedQuestion(false).WithAnswers(new List<Answer> { new Answer("10", true) }).Build(),
            new QuestionAggregateBuilder().WithTitle("Question 3").WithPointsForAnswer(5).WithClosedQuestion(false).WithPhoto(new Photo(new byte[] { 1, 2, 3 })).Build()
        };

        private IEnumerable<StudentAnswer> studentAnswers;

        public TestResultAggregateBuilder()
        {
            studentAnswers = new List<StudentAnswer>()
            {
                new StudentAnswer(questions.First(), new List<string>() { "Correct" }),
                new StudentAnswer(questions.ElementAt(1), new List<string>() { "7" }),
                new StudentAnswer(questions.First(), new List<string>()),
            };
        }

        protected override TestResultAggregate BuildInstance()
        {
            return TestResultAggregate.Create(test, student, studentAnswers);
        }

        public TestResultAggregateBuilder WithTest(TestAggregate test)
        {
            this.test = test;

            return this;
        }

        public TestResultAggregateBuilder WithStudent(Participient student)
        {
            this.student = student;

            return this;
        }

        public TestResultAggregateBuilder WithStudentAnswers(IEnumerable<StudentAnswer> studentAnswers)
        {
            this.studentAnswers = studentAnswers;

            return this;
        }
    }
}
