﻿using Chat.Application.Test.fakes;
using Chat.Database.ChatDatabase;
using Chat.Database.GroupChatAggregateDatabase;
using Chat.Database.PrivateChatAggregateDatabase;
using Core.Application.Test;
using Microsoft.Extensions.DependencyInjection;

namespace Chat.Application.Test.GroupConversationQueries
{
    public class ChatServicesMock : ServicesMock
    {
        protected IGroupChatAggregateRepository groupChatRepository = new GroupChatAggregateRepositoryFake();
        protected IPrivateChatAggregateRepository privateChatAggregateRepository = new PrivateChatAggregateRepositoryFake();
        protected IChatRepository chatRepository = new ChatRepositoryFake();

        protected ChatServicesMock()
        {
            var services = new ServiceCollection();

            services.AddSingleton(groupChatRepository);
            services.AddSingleton(privateChatAggregateRepository);
            services.AddSingleton(chatRepository);
        }
    }
}
