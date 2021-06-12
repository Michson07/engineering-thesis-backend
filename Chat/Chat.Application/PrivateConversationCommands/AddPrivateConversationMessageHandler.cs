using Chat.Database.PrivateChatAggregateDatabase;
using Chat.Domain.Aggregates;
using Chat.Domain.ValueObjects;
using Core.Api;
using Core.Application;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Chat.Application.PrivateConversationCommands
{
    public class AddPrivateConversationMessageHandler : IRequestHandler<AddPrivateConversationMessageDto, CommandResult>
    {
        private readonly IPrivateChatAggregateRepository repository;

        public AddPrivateConversationMessageHandler(IPrivateChatAggregateRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CommandResult> Handle(AddPrivateConversationMessageDto request, CancellationToken cancellationToken)
        {
            var chatExists = await repository.Get(request.SenderId, request.RecipientId);
            var message = new Message(request.SenderId, request.Message);

            if (chatExists == null)
            {
                var chat = PrivateChatAggregate.Create(request.SenderId, request.RecipientId, message);
                await repository.Add(chat);
            }
            else
            {
                var chat = chatExists.Update(message);
                repository.Update(chat);
            }

            await repository.SaveChanges();

            return new CommandResult { Result = new OkResult() };
        }
    }
}
