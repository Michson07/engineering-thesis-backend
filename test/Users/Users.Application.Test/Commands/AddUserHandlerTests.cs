using Core.Api;
using Core.Application.Exceptions;
using Core.Domain.ValueObjects;
using FluentValidation;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Users.Application.Commands;
using Users.Domain.Aggregates;
using Xunit;

namespace Users.Application.Test.Commands
{
    public class AddUserHandlerTests : UsersServicesMock
    {
        private readonly AddUserHandler handler;
        private readonly AddUserDto newUserDto = new AddUserDto
        {
            Email = "user@mail.com",
            Name = "Jan",
            LastName = "Nowak"
        };

        public AddUserHandlerTests()
        {
            handler = new AddUserHandler(repository);
        }

        [Fact]
        public async Task ShouldCreateUserAsync()
        {
            var response = await handler.Handle(newUserDto, CancellationToken.None);

            var users = await repository.GetAllUsersAsync();
            var expectedUser = UserAggregate.Create(new Email("user@mail.com"), "Jan", "Nowak", null);
            var addedUser = users.Single();

            Assert.IsType<OkResult>(response.Result);
            Assert.Equal(expectedUser.Email, addedUser.Email);
            Assert.Equal(expectedUser.Name, addedUser.Name);
            Assert.Equal(expectedUser.LastName, addedUser.LastName);
            Assert.Null(addedUser.Photo);
            Assert.True(users.Count() == 1);
        }

        [Fact]
        public async Task ShouldReturnConflictIsUserAlreadyExistsAsync()
        {
            repository.Add(UserAggregate.Create(new Email("user@mail.com"), "Jan", "Nowak", null));

            var ex = await Assert.ThrowsAsync<DomainException>(async () => await handler.Handle(newUserDto, CancellationToken.None));
            Assert.Equal($"Użytkownik z mailem {newUserDto.Email} już istnieje", ex.Message);

            var users = await repository.GetAllUsersAsync();
            Assert.True(users.Count() == 1);
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionWhenEmailIsEmpty()
        {
            var newUser = new AddUserDto
            {
                LastName = "Nowak",
                Name = "Michal",
            };

            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(newUser, CancellationToken.None));
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionWhenLastNameIsEmpty()
        {
            var newUser = new AddUserDto
            {
                Email = "user@mail.com",
                Name = "Michal",
            };

            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(newUser, CancellationToken.None));
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionWhenNameIsEmpty()
        {
            var newUser = new AddUserDto
            {
                Email = "user@mail.com",
                LastName = "Kowalski",
            };

            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(newUser, CancellationToken.None));
        }
    }
}
