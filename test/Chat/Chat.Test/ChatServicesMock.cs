﻿using Chat.Application.Test.fakes;
using Chat.Database.ChatDatabase;
using Chat.Database.GroupChatAggregateDatabase;
using Chat.Database.PrivateChatAggregateDatabase;
using Core.Application.Test;

namespace Chat.Application.Test
{
    public class ChatServicesMock : ServicesMock
    {
        protected IGroupChatAggregateRepository groupChatRepository = new GroupChatAggregateRepositoryFake();
        protected IPrivateChatAggregateRepository privateChatAggregateRepository = new PrivateChatAggregateRepositoryFake();
        protected IChatRepository chatRepository;

        protected ChatServicesMock()
        {
            chatRepository = new ChatRepositoryFake(groupChatRepository, (PrivateChatAggregateRepositoryFake)privateChatAggregateRepository);
        }
    }
}
