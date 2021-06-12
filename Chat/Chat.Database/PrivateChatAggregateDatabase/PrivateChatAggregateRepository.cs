using Chat.Domain.Aggregates;
using Core.Database;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Chat.Database.PrivateChatAggregateDatabase
{
    public class PrivateChatAggregateRepository : AggregateRepository<ChatDbContext>, IPrivateChatAggregateRepository
    {
        public PrivateChatAggregateRepository(ChatDbContext dbContext) : base(dbContext)
        {
        }

        public async Task Add(PrivateChatAggregate chat)
        {
            await dbContext.AddAsync(chat);
        }

        public async Task<PrivateChatAggregate?> Get(string senderId, string recipientId)
        {
            return await dbContext
                .PrivateChatAggregate
                .FirstOrDefaultAsync(chat => chat.User1Id.ToString() == senderId && chat.User2Id.ToString() == recipientId);
        }

        public void Update(PrivateChatAggregate chat)
        {
            dbContext.Update(chat);
        }
    }
}
