using Core.Domain;
using Core.Domain.ValueObjects;
using FluentValidation;
using Groups.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace Groups.Domain.Aggregates
{
    public class QuestionAggregate : AggregateRoot
    {
        public string Title { get; init; } = null!;
        public Photo? Photo { get; init; }
        public IEnumerable<Answer>? Answers { get; init; } = null!;
        public bool ManyCorrectAnswers { get; init; } = false;
        public bool ClosedQuestion { get; init; } = true;

        public int PointsForQuestion => Answers != null ? Answers.Count(a => a.Correct) * points : points;

        private readonly int points = 0;

        private QuestionAggregate()
        {
        }

        private QuestionAggregate(int points, string title, Photo? photo, IEnumerable<Answer>? answers,
            bool manyCorrectAnswers, bool closedQuestion)
        {
            this.points = points;
            Title = title;
            Photo = photo;
            Answers = answers;
            ManyCorrectAnswers = manyCorrectAnswers;
            ClosedQuestion = closedQuestion;
        }

        public static QuestionAggregate Create(int points, string title, Photo? photo, IEnumerable<Answer>? answers,
            bool manyCorrectAnswers = false, bool closedQuestion = true)
        {
            var question = new QuestionAggregate(points, title, photo, answers, manyCorrectAnswers, closedQuestion);
            new QuestionValidation().ValidateAndThrow(question);

            return question;
        }
    }

    public class QuestionValidation : AbstractValidator<QuestionAggregate>
    {
        public QuestionValidation()
        {
            RuleFor(question => question.PointsForQuestion).GreaterThan(0);
            RuleFor(question => question.Title).NotEmpty().NotNull();

            When(question => !question.ManyCorrectAnswers, () =>
            {
                RuleFor(question => question.Answers).Must(answers => answers!.Count(a => a.Correct) == 1);
            });

            When(question => question.ManyCorrectAnswers, () =>
            {
                RuleFor(question => question.Answers).NotEmpty().NotNull();

                RuleFor(question => question.Answers)
                    .Must(answers => answers!.Count() >= answers!.Count(a => a.Correct));

                RuleFor(question => question.Answers)
                    .Must(answers => answers!.Any(a => a.Correct));
            });
        }
    }
}
