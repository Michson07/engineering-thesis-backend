using Chat.Database.GroupChatAggregateDatabase;
using Chat.Domain.Aggregates;
using Core.Application;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Chat.Application.GroupConversationQueries
{
    public class GetGroupConversationHandler : IRequestHandler<GroupConversationDto, QueryResult<List<MessageView>>>
    {
        private readonly IGroupChatAggregateRepository repository;

        public GetGroupConversationHandler(IGroupChatAggregateRepository repository)
        {
            this.repository = repository;
        }

        public async Task<QueryResult<List<MessageView>>> Handle(GroupConversationDto request, CancellationToken cancellationToken)
        {
            var chatExists = await repository.Get(request.GroupId);

            var view = MapToView(chatExists);

            return new QueryResult<List<MessageView>> { BodyResponse = view };
        }

        private List<MessageView> MapToView(GroupChatAggregate? chat)
        {
            var view = new List<MessageView>();
            if (chat != null)
            {
                foreach (var message in chat.Messages)
                {
                    view.Add(new MessageView
                    {
                        User = message.UserEmail.ToString(),
                        Text = message.Text,
                        Date = message.Date
                    });
                }
            }

            return view;
        }
    }
}
