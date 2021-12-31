using Core.Database.Test;
using Core.Domain.ValueObjects;
using FluentAssertions;
using System.Linq;
using System.Threading.Tasks;
using Users.Database.UserAggregateDatabase;
using Users.Domain.Aggregates;
using Xunit;

namespace Users.Database.Test.UserAggregateDatabase
{
    public class UserAggregateRepositoryTests : DatabaseTestConfiguration<UserAggregateDbContext>
    {
        private readonly IUserAggregateRepository userRepository;

        public UserAggregateRepositoryTests()
        {
            userRepository = new UserAggregateRepository(dbContext);
        }

        [Fact]
        public async Task ShouldAddUserToDatabase()
        {
            var user = GivenUser();

            userRepository.Add(user);
            await userRepository.SaveChanges();

            var userInDb = dbContext.UserAggregate.Single();
            userInDb.Should().BeEquivalentTo(user);
        }

        [Fact]
        public async Task ShouldGetUserByEmail()
        {
            var email = new Email("findEmail@mail.com");
            var user = GivenUser(email: email);

            dbContext.UserAggregate.Add(user);
            await userRepository.SaveChanges();

            var userInDb = userRepository.Get(email);
            userInDb.Should().BeEquivalentTo(user);
        }

        [Fact]
        public async Task ShouldGetAllUsers()
        {
            var users = new UserAggregate[]
            {
                GivenUser(new("user1@mail.com"), "user1", "user1"),
                GivenUser(new("user2@mail.com"), "user2", "user2"),
                GivenUser(new("user3@mail.com"), "user3", "user3")
            };

            dbContext.UserAggregate.AddRange(users);
            await userRepository.SaveChanges();

            var usersInDb = await userRepository.GetAllUsersAsync();
            usersInDb.Should().BeEquivalentTo(users);
        }

        private static UserAggregate GivenUser(Email email = null, string name = null, string lastname = null)
        {
            return UserAggregate.Create(
                email ?? new("user@email.com"),
                name ?? "Jan",
                lastname ?? "Nowak",
                null
            );
        }
    }
}
