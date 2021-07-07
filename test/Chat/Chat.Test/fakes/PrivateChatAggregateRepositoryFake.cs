using Chat.Database.PrivateChatAggregateDatabase;
using Chat.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Application.Test.fakes
{
    public class PrivateChatAggregateRepositoryFake : IPrivateChatAggregateRepository
    {
        private readonly List<PrivateChatAggregate> chats = new List<PrivateChatAggregate>();

        public Task Add(PrivateChatAggregate chat)
        {
            chats.Add(chat);

            return Task.CompletedTask;
        }

        public Task<PrivateChatAggregate> Get(string senderEmail, string recipientEmail)
        {
            var foundChat = chats.FirstOrDefault(chat =>
                    chat.User1Email == senderEmail && chat.User2Email == recipientEmail
                    || chat.User1Email == recipientEmail && chat.User2Email == senderEmail);

            return Task.FromResult(foundChat);
        }

        public Task<PrivateChatAggregate> GetById(Guid id)
        {
            var foundChat = chats.FirstOrDefault(chat => chat.Id == id);

            return Task.FromResult(foundChat);
        }

        public Task SaveChanges()
        {
            return Task.CompletedTask;
        }

        public void Update(PrivateChatAggregate chat)
        {
            //not needed
        }

        public IEnumerable<PrivateChatAggregate> GetPrivateChats(string userEmail)
        {
            return chats
                .Where(chat => chat.User1Email == userEmail || chat.User2Email == userEmail);
        }
    }
}
