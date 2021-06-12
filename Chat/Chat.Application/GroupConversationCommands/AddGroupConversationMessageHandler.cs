using Chat.Database.GroupChatAggregateDatabase;
using Chat.Domain.Aggregates;
using Chat.Domain.ValueObjects;
using Core.Api;
using Core.Application;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Chat.Application.GroupConversationCommands
{
    public class AddGroupConversationMessageHandler : IRequestHandler<AddGroupConversationMessageDto, CommandResult>
    {
        private readonly IGroupChatAggregateRepository repository;

        public AddGroupConversationMessageHandler(IGroupChatAggregateRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CommandResult> Handle(AddGroupConversationMessageDto request, CancellationToken cancellationToken)
        {
            var groupChatExists = await repository.Get(request.GroupId);
            var message = new Message(request.SenderId, request.Message);

            if (groupChatExists == null)
            {
                var groupChat = GroupChatAggregate.Create(request.GroupId, message);
                await repository.Add(groupChat);
            }
            else
            {
                var groupChat = groupChatExists.Update(message);
                repository.Update(groupChat);
            }

            await repository.SaveChanges();

            return new CommandResult { Result = new OkResult() };
        }
    }
}
