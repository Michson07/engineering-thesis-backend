using Chat.Database.ChatDatabase;
using Chat.Database.GroupChatAggregateDatabase;
using Chat.Database.PrivateChatAggregateDatabase;
using Chat.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Application.Test.fakes
{
    public class ChatRepositoryFake : IChatRepository
    {
        private readonly IGroupChatAggregateRepository groupChatAggregateRepository;
        private readonly PrivateChatAggregateRepositoryFake privateChatAggregateRepository = new PrivateChatAggregateRepositoryFake();

        public Task<IEnumerable<ConversationBasic>> Get(string userEmail, IEnumerable<Guid> groupsIds)
        {
            var groupsChats = GetGroupChats(groupsIds);
            var privateChats = privateChatAggregateRepository.GetPrivateChats(userEmail);

            var chats = groupsChats
                .Select(groupChat => new ConversationBasic(
                    groupChat.GroupId, true, false, groupChat.Messages.Last().Text, groupChat.Messages.Last().Date))
                .Union(privateChats.Select(privateChat => new ConversationBasic(
                    privateChat.Id, false, true, privateChat.Messages.Last().Text, privateChat.Messages.Last().Date)));
            
            return Task.FromResult(chats);
        }

        private IEnumerable<GroupChatAggregate> GetGroupChats(IEnumerable<Guid> groupsIds)
        {
            var chats = new List<GroupChatAggregate>();
            foreach (var groupId in groupsIds)
            {
                var group = groupChatAggregateRepository.Get(groupId.ToString()).Result;

                if (group != null)
                {
                    chats.Add(group);
                }
            }

            return chats;
        }
    }
}
