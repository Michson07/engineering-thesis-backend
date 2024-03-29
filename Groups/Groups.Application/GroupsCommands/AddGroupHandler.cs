﻿using Core.Api;
using Core.Application;
using Core.Application.Exceptions;
using Core.Domain.ValueObjects;
using Groups.Database.GroupsAggregateDatabase;
using Groups.Domain;
using Groups.Domain.Aggregates;
using Groups.Domain.ValueObjects;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Groups.Application.GroupsCommands
{
    public class AddGroupHandler : IRequestHandler<AddGroupDto, CommandResult>
    {
        private readonly IGroupAggregateRepository repository;

        public AddGroupHandler(IGroupAggregateRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CommandResult> Handle(AddGroupDto request, CancellationToken cancellationToken)
        {
            var groupExists = await repository.GetByName(request.Name);

            if (groupExists != null)
            {
                throw new DomainException($"Grupa {request.Name} już istnieje");
            }

            var owner = new Participient(new Email(request.OwnerEmail), GroupRoles.Owner);
            var group = GroupAggregate.Create(new List<Participient>() { owner },
                new GroupName(request.Name),
                request.Description, request.Open);

            repository.Add(group);
            await repository.SaveChanges();

            return new CommandResult { Result = new OkResult() };
        }
    }
}
