using Chat.Database.PrivateChatAggregateDatabase;
using Chat.Domain.Aggregates;
using Core.Application;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Chat.Application.PrivateConversationQueries
{
    public class GetPrivateConversationHandler : IRequestHandler<PrivateConversationDto, QueryResult<List<MessageView>>>
    {
        private readonly IPrivateChatAggregateRepository repository;

        public GetPrivateConversationHandler(IPrivateChatAggregateRepository repository)
        {
            this.repository = repository;
        }

        public async Task<QueryResult<List<MessageView>>> Handle(PrivateConversationDto request, CancellationToken cancellationToken)
        {
            var chatExists = await repository.Get(request.SenderEmail, request.RecipientEmail);

            var view = MapToView(chatExists);

            return new QueryResult<List<MessageView>> { BodyResponse = view };
        }

        private List<MessageView> MapToView(PrivateChatAggregate? chat)
        {
            var view = new List<MessageView>();
            if (chat != null)
            {
                foreach (var message in chat.Messages)
                {
                    view.Add(new MessageView
                    {
                        User = message.UserEmail,
                        Text = message.Text,
                        Date = message.Date
                    });
                }
            }

            return view;
        }
    }
}
