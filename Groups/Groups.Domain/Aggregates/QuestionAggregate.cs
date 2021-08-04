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
        public IEnumerable<Answer>? Answers { get; init; }
        public bool ClosedQuestion { get; init; } = true;
        public int PointsForAnswer { get; init; } = 0;

        public int PointsForQuestion => Answers != null ? Answers.Count(a => a.Correct) * PointsForAnswer : PointsForAnswer;
        public bool ManyCorrectAnswers => Answers != null && Answers.Count(a => a.Correct) > 1;


        private QuestionAggregate()
        {
        }

        private QuestionAggregate(int pointsForAnswer, string title, Photo? photo, IEnumerable<Answer>? answers,
            bool closedQuestion)
        {
            PointsForAnswer = pointsForAnswer;
            Title = title;
            Photo = photo;
            Answers = answers != null && answers.Any() ? answers : null;
            ClosedQuestion = closedQuestion;
        }

        public static QuestionAggregate Create(int pointsForAnswer, string title, Photo? photo, IEnumerable<Answer>? answers,
            bool closedQuestion = true)
        {
            var question = new QuestionAggregate(pointsForAnswer, title, photo, answers, closedQuestion);
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

            When(question => !question.ManyCorrectAnswers && question.Answers != null, () =>
            {
                RuleFor(question => question.Answers).Must(answers => answers!.Count(a => a.Correct) == 1);
            });

            When(question => question.ClosedQuestion, () =>
            {
                RuleFor(question => question.Answers).Must(answers => answers!.Count() >= 2);
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
