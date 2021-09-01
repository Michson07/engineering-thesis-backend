using Core.Api;
using Core.Application;
using Core.Application.Exceptions;
using Core.Domain.ValueObjects;
using Groups.Database.GroupsAggregateDatabase;
using Groups.Database.QuestionAggregateDatabase;
using Groups.Database.TestAggregateDatabase;
using Groups.Domain.Aggregates;
using Groups.Domain.ValueObjects;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Groups.Application.TestCommands
{
    public class AddTestHandler : IRequestHandler<AddTestDto, CommandResult>
    {
        private readonly IGroupAggregateRepository groupRepository;
        private readonly ITestAggregateRepository testRepository;
        private readonly IQuestionAggregateRepository questionRepository;

        public AddTestHandler(IGroupAggregateRepository groupRepository, ITestAggregateRepository testRepository, IQuestionAggregateRepository questionRepository)
        {
            this.groupRepository = groupRepository;
            this.testRepository = testRepository;
            this.questionRepository = questionRepository;
        }

        public async Task<CommandResult> Handle(AddTestDto request, CancellationToken cancellationToken)
        {
            var group = groupRepository.GetById(request.Group);
            if (group == null)
            {
                throw new NotFoundException(request.Group, "grupy");
            }

            var questions = MapToQuestions(request.Questions);
            var test = TestAggregate.Create(request.Name, questions, group,
                request.Date.ToLocalTime(), request.RequirePhoto, request.PassedFrom, request.TimeDuration);

            await questionRepository.Add(questions);
            await testRepository.Add(test);
            await testRepository.SaveChanges();

            return new CommandResult { Result = new OkResult() };
        }

        private IEnumerable<QuestionAggregate> MapToQuestions(IEnumerable<QuestionDto> questions)
        {
            var questionAggregates = new List<QuestionAggregate>();
            foreach (var question in questions)
            {
                IEnumerable<Answer>? answers = null;
                if (question.Answers != null)
                {
                    answers = question.Answers.Select(a => new Answer(a.Value, a.Correct)).ToList();
                }

                var photo = question.Photo != null ? new Photo(question.Photo) : null;
                questionAggregates.Add(
                    QuestionAggregate.Create(question.Points, question.Title, photo,
                        answers, question.ClosedQuestion)
                );
            }

            return questionAggregates;
        }
    }
}
