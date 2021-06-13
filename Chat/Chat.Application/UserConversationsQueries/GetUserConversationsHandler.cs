using Chat.Database.ChatDatabase;
using Chat.Database.PrivateChatAggregateDatabase;
using Core.Application;
using Groups.Application.GroupsQueries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Users.Application.Queries;

namespace Chat.Application.UserConversationsQueries
{
    public class GetUserConversationsHandler : IRequestHandler<GetUserConverstationsDto, QueryResult<List<UserConversationsView>>>
    {
        private readonly IChatRepository repository;
        private readonly IPrivateChatAggregateRepository privateChatRepository;
        private readonly IMediator mediator;

        public GetUserConversationsHandler(
            IChatRepository repository,
            IMediator mediator,
            IPrivateChatAggregateRepository privateChatRepository)
        {
            this.repository = repository;
            this.mediator = mediator;
            this.privateChatRepository = privateChatRepository;
        }

        public async Task<QueryResult<List<UserConversationsView>>> Handle(GetUserConverstationsDto request, CancellationToken cancellationToken)
        {
            var userGroups = await mediator.Send(new GetUserGroupsDto { Email = request.UserEmail });
            var groupsIds = userGroups.BodyResponse != null
                ? userGroups.BodyResponse.Select(group => new Guid(group.Id))
                : new List<Guid>();

            var conversations = await repository.Get(request.UserEmail, groupsIds);

            var privateConversationsView = await GetPrivateConversationsViews(
                conversations.Where(c => c.EntityIsPrivate),
                request.UserEmail
            );

            var groupConversationsView = GetGroupConversationsViews(
                conversations.Where(c => c.EntityIsGroup),
                userGroups.BodyResponse!);

            return new QueryResult<List<UserConversationsView>>
            {
                BodyResponse = privateConversationsView.Concat(groupConversationsView).ToList()
            };
        }

        private IEnumerable<UserConversationsView> GetGroupConversationsViews(
            IEnumerable<ConversationBasic> chats,
            IEnumerable<GroupView> userGroups)
        {
            var conversations = new List<UserConversationsView>();
            foreach (var chat in chats)
            {
                var groupName = userGroups.FirstOrDefault(group => group.Id == chat.EntityId.ToString())?.Name;
                if (groupName != null)
                {
                    conversations.Add(new UserConversationsView
                    {
                        GroupId = chat.EntityId.ToString(),
                        LastMessage = chat.LastMessage,
                        LastMessageDate = chat.LastMessageDate,
                        Name = groupName
                    });
                }
            }

            return conversations;
        }

        private async Task<IEnumerable<UserConversationsView>> GetPrivateConversationsViews(
            IEnumerable<ConversationBasic> chats,
            string currentUserEmail
            )
        {
            var conversations = new List<UserConversationsView>();
            foreach (var chat in chats)
            {
                var chatAggregate = await privateChatRepository.GetById(chat.EntityId);
                if (chatAggregate != null)
                {
                    var secondUserEmail = chatAggregate.User1Email == currentUserEmail ? chatAggregate.User2Email : chatAggregate.User1Email;
                    var user = await mediator.Send(new GetUserByEmailDto { Email = secondUserEmail });

                    if (user.BodyResponse != null)
                    {
                        var name = user.BodyResponse.Name;
                        var lastName = user.BodyResponse.LastName;

                        conversations.Add(new UserConversationsView
                        {
                            RecipientEmail = secondUserEmail,
                            Name = $"{name} {lastName}",
                            LastMessage = chat.LastMessage,
                            LastMessageDate = chat.LastMessageDate
                        });
                    }
                }
            }

            return conversations;
        }
    }
}
