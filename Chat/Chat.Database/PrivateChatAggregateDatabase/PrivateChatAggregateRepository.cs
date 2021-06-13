using Chat.Domain.Aggregates;
using Core.Database;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<PrivateChatAggregate?> Get(string senderEmail, string recipientEmail)
        {
            return await dbContext
                .PrivateChatAggregate
                .FirstOrDefaultAsync(chat => chat.User1Email.ToString() == senderEmail && chat.User2Email.ToString() == recipientEmail);
        }

        public async Task<PrivateChatAggregate?> GetById(Guid id)
        {
            return await dbContext
                .PrivateChatAggregate
                .FirstOrDefaultAsync(chat => chat.Id == id);
        }

        public void Update(PrivateChatAggregate chat)
        {
            dbContext.Update(chat);
        }
    }
}
