using Core.Api;
using Core.Application;
using Groups.Application.TestQueries;
using MediatR;
using Notifications.Application.NotificationCommands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Groups.Application.TestCommands
{
    public class SendNotificationsAboutTomorrowTestHandler : IRequestHandler<SendNotificationsAboutTomorrowTest, CommandResult>
    {
        private readonly IMediator mediator;

        public SendNotificationsAboutTomorrowTestHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<CommandResult> Handle(SendNotificationsAboutTomorrowTest request, CancellationToken cancellationToken)
        {
            var tomorrowTests = await mediator.Send(new GetTestsInTimePeriodDto { Time = DateTime.Now });
            if (tomorrowTests.BodyResponse != null)
            {
                foreach (var test in tomorrowTests.BodyResponse)
                {
                    await mediator.Send(new CreateEventNotificationsDto
                    {
                        EventId = test.TestId,
                        Title = $"Test {test.TestName} już jutro!",
                        Message = $"Jutro odbędzie się test {test.TestName}. Proszę się do niego odpowiednio przygotować.\n " +
                            $"Dodatkowe informacje:\n" +
                            $"Wymagane potwierdzenie twarzy: {TranslateToPolish(test.TestRequirePhoto)}\n" +
                            $"Rozpoczęcie: {test.TestDate}\n" +
                            $"Czas trwania: {test.TestTimeDuration}",
                        Emails = test.Emails
                    });
                }
            }


            return new CommandResult { Result = new NoContentResult() };
        }

        private string TranslateToPolish(bool requirePhoto)
        {
            return requirePhoto ? "Tak" : "Nie";
        }
    }
}
