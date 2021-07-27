using Core.Domain.ValueObjects;
using FluentValidation;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Users.Application.Commands;
using Users.Domain.Aggregates;
using Xunit;

namespace Users.Application.Test.Commands
{
    public class UpdateUserHandlerTests : UsersServicesMock
    {
        private readonly UpdateUserHandler handler;

        public UpdateUserHandlerTests()
        {
            repository.Add(UserAggregate.Create(new Email("user@mail.com"), "Jan", "Nowak", new Photo(new byte[] { 0, 0, 0, 25 })));
            handler = new UpdateUserHandler(repository, mediator);
        }

        [Fact]
        public async Task ShouldUpdateUserAsync()
        {
            var updateUserDto = new UpdateUserDto
            {
                Email = "user@mail.com",
                Name = "Michal",
                LastName = "Kowalski",
                Photo = null
            };

            await handler.Handle(updateUserDto, CancellationToken.None);
            var users = await repository.GetAllUsersAsync();
            var expectedUser = UserAggregate.Create(new Email("user@mail.com"), "Michal", "Kowalski", null);
            var updatedUser = users.Single();

            Assert.Equal(expectedUser.Email, updatedUser.Email);
            Assert.Equal(expectedUser.Name, updatedUser.Name);
            Assert.Equal(expectedUser.LastName, updatedUser.LastName);
            Assert.Null(updatedUser.Photo);
            Assert.True(users.Count() == 1);
        }

        [Fact]
        public async Task ShouldCreateNewUserIfNotExistsAsync()
        {
            var updateUserDto = new UpdateUserDto
            {
                Email = "user2@mail.com",
                Name = "Michal",
                LastName = "Kowalski",
                Photo = null
            };

            await handler.Handle(updateUserDto, CancellationToken.None);

            await mediator.Received(1).Send(Arg.Any<AddUserDto>());
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionWhenLastNameIsEmpty()
        {
            var updateUserDto = new UpdateUserDto
            {
                Email = "user@mail.com",
                Name = "Michal",
                Photo = null
            };

            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(updateUserDto, CancellationToken.None));
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionWhenNameIsEmpty()
        {
            var updateUserDto = new UpdateUserDto
            {
                Email = "user@mail.com",
                LastName = "Kowalski",
                Photo = null
            };

            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(updateUserDto, CancellationToken.None));
        }
    }
}
