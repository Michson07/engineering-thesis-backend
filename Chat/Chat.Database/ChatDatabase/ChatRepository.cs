using Chat.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Database.ChatDatabase
{
    public class ChatRepository : IChatRepository
    {
        private readonly ChatDbContext dbContext;

        public ChatRepository(ChatDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<ConversationBasic>> Get(string userEmail, IEnumerable<Guid> groupsIds)
        {
            var groupsChats = await GetGroupChatsAsync(groupsIds);
            var privateChats = await GetPrivateChatsAsync(userEmail);

            return groupsChats
                .Select(groupChat => new ConversationBasic(
                    groupChat.GroupId, true, false, groupChat.Messages.Last().Text, groupChat.Messages.Last().Date))
                .Union(privateChats.Select(privateChat => new ConversationBasic(
                    privateChat.Id, false, true, privateChat.Messages.Last().Text, privateChat.Messages.Last().Date)));
        }

        private async Task<IEnumerable<GroupChatAggregate>> GetGroupChatsAsync(IEnumerable<Guid> groupsIds)
        {
            var chats = new List<GroupChatAggregate>();
            foreach (var groupId in groupsIds)
            {
                var group = await dbContext
                    .GroupChatAggregate
                    .FirstOrDefaultAsync(chat => chat.GroupId == groupId);

                if (group != null)
                {
                    chats.Add(group);
                }
            }

            return chats;
        }
        private async Task<IEnumerable<PrivateChatAggregate>> GetPrivateChatsAsync(string userEmail)
        {
            return await dbContext
                .PrivateChatAggregate
                .Where(chat => chat.User1Email == userEmail || chat.User2Email == userEmail)
                .ToListAsync();
        }
    }
}
