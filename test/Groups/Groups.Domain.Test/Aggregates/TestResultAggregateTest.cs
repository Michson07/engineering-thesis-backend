using Groups.Domain.Aggregates;
using Groups.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Groups.Domain.Test.Aggregates
{
    public class TestResultAggregateTest
    {
        private TestResultAggregate testResult;

        public TestResultAggregateTest()
        {
            testResult = new TestResultAggregateBuilder().Build();
        }

        [Fact]
        public void ShouldCreateTestResult()
        {
            Assert.True(testResult.StudentAnswers.Count() == 3);
            Assert.Equal(CheckedState.NotChecked, testResult.Checked);
            Assert.True(testResult.ReceivedPoints == 2);
            Assert.Equal("NotPassed", testResult.State);
        }

        [Fact]
        public void ShouldCreateTestResultForAbsentStudent()
        {
            var testResultForAbsent = TestResultAggregate.CreateForAbsent(testResult.Test, testResult.Student);

            Assert.True(testResultForAbsent.ReceivedPoints == 0);
            Assert.Equal("NotPassed", testResult.State);
            Assert.All(testResultForAbsent.StudentAnswers,
                sa =>
                {
                    Assert.True(sa.ReceivedAnswers.Count() == 1);
                    Assert.Equal(string.Empty, sa.ReceivedAnswers.First());
                }
            );
        }

        [Fact]
        public void ShouldUpdateTestResult()
        {
            var openStudentAnswer = testResult.StudentAnswers.Last();
            openStudentAnswer = new StudentAnswer(openStudentAnswer.Question, openStudentAnswer.ReceivedAnswers, 3);
            var checkedStudentAnswers = new List<StudentAnswer>()
            {
                testResult.StudentAnswers.First(),
                testResult.StudentAnswers.ElementAt(1),
                openStudentAnswer
            };

            testResult.Update(testResult.Test, testResult.Student, checkedStudentAnswers);
            Assert.True(testResult.StudentAnswers.Count() == 3);
            Assert.Equal(CheckedState.Checked, testResult.Checked);
            Assert.True(testResult.ReceivedPoints == 5);
            Assert.Equal("NotPassed", testResult.State);
        }
    }
}
