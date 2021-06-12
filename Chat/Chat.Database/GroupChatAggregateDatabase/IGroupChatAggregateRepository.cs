using Chat.Domain.Aggregates;
using Core.Database;
using System.Threading.Tasks;

namespace Chat.Database.GroupChatAggregateDatabase
{
    public interface IGroupChatAggregateRepository : IAggregateRepository
    {
        Task Add(GroupChatAggregate groupChat);
        void Update(GroupChatAggregate groupChat);
        Task<GroupChatAggregate?> Get(string groupId);
    }
}
