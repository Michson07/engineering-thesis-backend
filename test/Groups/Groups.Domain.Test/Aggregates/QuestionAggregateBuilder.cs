using Core.Domain.Test;
using Core.Domain.ValueObjects;
using Groups.Domain.Aggregates;
using Groups.Domain.ValueObjects;
using System.Collections.Generic;

namespace Groups.Domain.Test.Aggregates
{
    public class QuestionAggregateBuilder : Builder<QuestionAggregate>
    {
        private string title = "Question 1";
        private Photo? photo = null;
        private IEnumerable<Answer>? answers = null;
        private bool closedQuestion = false;
        private int pointsForAnswer = 2;

        protected override QuestionAggregate BuildInstance()
        {
            return QuestionAggregate.Create(pointsForAnswer, title, photo, answers, closedQuestion);
        }

        public QuestionAggregateBuilder WithTitle(string title)
        {
            this.title = title;

            return this;
        }

        public QuestionAggregateBuilder WithPhoto(Photo? photo)
        {
            this.photo = photo;

            return this;
        }

        public QuestionAggregateBuilder WithAnswers(IEnumerable<Answer>? answers)
        {
            this.answers = answers;

            return this;
        }

        public QuestionAggregateBuilder WithClosedQuestion(bool closedQuestion)
        {
            this.closedQuestion = closedQuestion;

            return this;
        }

        public QuestionAggregateBuilder WithPointsForAnswer(int pointsForAnswer)
        {
            this.pointsForAnswer = pointsForAnswer;

            return this;
        }
    }
}
