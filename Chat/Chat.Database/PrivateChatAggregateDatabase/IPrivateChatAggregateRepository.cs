using Chat.Domain.Aggregates;
using Core.Database;
using System;
using System.Threading.Tasks;

namespace Chat.Database.PrivateChatAggregateDatabase
{
    public interface IPrivateChatAggregateRepository : IAggregateRepository
    {
        Task Add(PrivateChatAggregate chat);
        void Update(PrivateChatAggregate chat);
        Task<PrivateChatAggregate?> Get(string senderEmail, string recipientEmail);
        Task<PrivateChatAggregate?> GetById(Guid id);
    }
}
