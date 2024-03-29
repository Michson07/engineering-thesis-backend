﻿using Core.Api;
using Core.Application.Exceptions;
using Groups.Application.GroupsCommands;
using Groups.Domain.Test.Aggregates;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Groups.Application.Test.GroupsCommands
{
    public class AddGroupHandlerTest : GroupsServicesMock
    {
        private readonly AddGroupHandler handler;

        public AddGroupHandlerTest()
        {
            handler = new AddGroupHandler(groupAggregateRepository);
        }

        [Fact]
        public async Task ShouldCreateGroupAsync()
        {
            var request = new AddGroupDto
            {
                Name = "Group",
                Description = "Some description",
                Open = true,
                OwnerEmail = "owner@email.com"
            };

            var response = await handler.Handle(request, CancellationToken.None);

            Assert.IsType<OkResult>(response.Result);
            Assert.NotNull(groupAggregateRepository.GetByName("Group"));
        }

        [Fact]
        public async Task ShouldReturnConflictResultWhenGroupExists()
        {
            var group = new GroupAggregateBuilder().WithGroupName("Group").Build();
            groupAggregateRepository.Add(group);

            var request = new AddGroupDto
            {
                Name = "Group",
            };

            var ex = await Assert.ThrowsAsync<DomainException>(() => handler.Handle(request, CancellationToken.None));
            Assert.Equal("Grupa Group już istnieje", ex.Message);
        }
    }
}
