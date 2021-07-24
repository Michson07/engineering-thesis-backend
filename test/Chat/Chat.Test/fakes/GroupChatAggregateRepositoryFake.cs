using Chat.Database.GroupChatAggregateDatabase;
using Chat.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Application.Test.fakes
{
    public class GroupChatAggregateRepositoryFake : IGroupChatAggregateRepository
    {
        private readonly List<GroupChatAggregate> chats = new List<GroupChatAggregate>();

        public Task Add(GroupChatAggregate groupChat)
        {
            chats.Add(groupChat);
            return Task.CompletedTask;
        }

        public Task<GroupChatAggregate> Get(string groupId)
        {
            var chat = chats.FirstOrDefault(c => c.GroupId.ToString().Equals(groupId, StringComparison.OrdinalIgnoreCase));

            return Task.FromResult(chat);
        }

        public Task SaveChanges()
        {
            return Task.CompletedTask;
        }

        public void Update(GroupChatAggregate groupChat)
        {
            //not needed
        }
    }
}
