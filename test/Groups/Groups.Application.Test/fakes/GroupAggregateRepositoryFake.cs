using Groups.Database.GroupsAggregateDatabase;
using Groups.Domain.Aggregates;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Groups.Application.Test.fakes
{
    public class GroupAggregateRepositoryFake : IGroupAggregateRepository
    {
        private readonly List<GroupAggregate> repository = new List<GroupAggregate>();

        public void Add(GroupAggregate group)
        {
            repository.Add(group);
        }

        public Task<GroupAggregate> GetByCode(string code)
        {
            return Task.FromResult(repository.FirstOrDefault(group => group.Code == code));
        }

        public GroupAggregate GetById(string id)
        {
            return repository.FirstOrDefault(group => group.Id.ToString() == id);
        }

        public Task<GroupAggregate> GetByName(string name)
        {
            return Task.FromResult(repository.FirstOrDefault(group => group.GroupName == name));
        }

        public Task<IEnumerable<GroupAggregate>> GetUserGroups(string email)
        {
            return Task.FromResult(repository.Where(group => group.Participients.Any(user => user.Email == email)));
        }

        public Task SaveChanges()
        {
            return Task.CompletedTask;
        }

        public void Update(GroupAggregate group)
        {
            //
        }
    }
}
