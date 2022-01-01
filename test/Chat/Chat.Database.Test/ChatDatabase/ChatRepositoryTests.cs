using Chat.Database.ChatDatabase;
using Chat.Domain.Aggregates;
using Chat.Domain.ValueObjects;
using Core.Database.Test;
using FluentAssertions;
using Groups.Domain;
using Groups.Domain.Test.Aggregates;
using Groups.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Chat.Database.Test.ChatDatabase
{
    public class ChatRepositoryTests : DatabaseTestConfiguration<ChatDbContext>
    {
        private readonly IChatRepository chatRepository;

        public ChatRepositoryTests()
        {
            chatRepository = new ChatRepository(dbContext);
        }

        [Fact]
        public async Task ShouldGetUserConversationsFromDatabase()
        {
            var userEmail = "someEmail@mail.com";
            var group1 = new GroupAggregateBuilder()
                .WithParticipients(new[] { new Participient(new(userEmail), GroupRoles.Owner) })
                .WithGroupName("group 1")
                .Build();

            var group2 = new GroupAggregateBuilder()
                .WithParticipients(new[] { new Participient(new(userEmail), GroupRoles.Owner) })
                .WithGroupName("group 2")
                .Build();

            var chat1Messages = GivenMessages(1, userEmail);
            var chat1 = GivenGroupChat(group1.Id.ToString(), chat1Messages);

            var chat2Messages = GivenMessages(5, userEmail);
            var chat2 = GivenGroupChat(group2.Id.ToString(), chat2Messages);

            var chat3SecondUser = "myFriend@mail.com";
            var chat3Messages1 = GivenMessages(2, userEmail);
            var chat3Messages2 = GivenMessages(2, chat3SecondUser);

            var chat3 = GivenPrivateChat(userEmail, chat3SecondUser, chat3Messages1.Concat(chat3Messages2));

            dbContext.AddRange(chat1, chat2);
            dbContext.Add(chat3);
            await dbContext.SaveChangesAsync();

            var chatsInDb = await chatRepository.Get(userEmail, new[] { group1.Id, group2.Id });

            chatsInDb.Should().BeEquivalentTo(new ConversationBasic[]
            {
                new ConversationBasic(group1.Id, true, false, chat1.Messages.Last().Text, chat1.Messages.Last().Date),
                new ConversationBasic(group2.Id, true, false, chat2.Messages.Last().Text, chat2.Messages.Last().Date),
                new ConversationBasic(chat3.Id, false, true, chat3.Messages.Last().Text, chat3.Messages.Last().Date),
            });
        }

        private static GroupChatAggregate GivenGroupChat(string groupId, IEnumerable<Message> messages)
        {
            var chat = GroupChatAggregate.Create(groupId, messages.First());

            for(var i = 1; i < messages.Count(); i++)
            {
                chat.Update(messages.ElementAt(i));
            }

            return chat;
        }

        private static PrivateChatAggregate GivenPrivateChat(string user1Email, string user2Email, IEnumerable<Message> messages)
        {
            var chat = PrivateChatAggregate.Create(new(user1Email), new(user2Email), messages.First());

            for (var i = 1; i < messages.Count(); i++)
            {
                chat.Update(messages.ElementAt(i));
            }

            return chat;
        }

        private static IEnumerable<Message> GivenMessages(int numberOfMessages, string userMail)
        {
            var messages = new List<Message>();

            for(int i = 0; i < numberOfMessages; i++)
            {
                messages.Add(new(new(userMail), Guid.NewGuid().ToString()));
            }

            return messages;
        }
    }
}
