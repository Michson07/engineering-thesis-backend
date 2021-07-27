using Core.Application.Test;
using Users.Database.UserAggregateDatabase;

namespace Users.Application.Test
{
    public class UsersServicesMock : ServicesMock
    {
        protected IUserAggregateRepository repository = new UserAggregateRepositoryFake();

    }
}
