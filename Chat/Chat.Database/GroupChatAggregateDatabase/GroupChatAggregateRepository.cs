using Chat.Domain.Aggregates;
using Core.Database;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Chat.Database.GroupChatAggregateDatabase
{
    public class GroupChatAggregateRepository : AggregateRepository<ChatDbContext>, IGroupChatAggregateRepository
    {
        public GroupChatAggregateRepository(ChatDbContext dbContext) : base(dbContext)
        {
        }

        public async Task Add(GroupChatAggregate groupChat)
        {
            await dbContext.AddAsync(groupChat);
        }

        public async Task<GroupChatAggregate?> Get(string groupId)
        {
            return await dbContext.GroupChatAggregate.FirstOrDefaultAsync(chat => chat.GroupId.ToString() == groupId);
        }

        public void Update(GroupChatAggregate groupChat)
        {
            dbContext.Update(groupChat);
        }
    }
}
