using Core.Api;
using Core.Application;
using Groups.Database.AnnouncementAggregateDatabase;
using Groups.Database.GroupsAggregateDatabase;
using Groups.Domain.Aggregates;
using Groups.Domain.ValueObjects;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Groups.Application.AnnouncementCommands
{
    public class AddAnnouncementHandler : IRequestHandler<AddAnnouncementDto, CommandResult>
    {
        private readonly IAnnouncementAggregateRepository announcemenRepository;
        private readonly IGroupAggregateRepository groupRepository;

        public AddAnnouncementHandler(IAnnouncementAggregateRepository announcemenRepository, IGroupAggregateRepository groupRepository)
        {
            this.announcemenRepository = announcemenRepository;
            this.groupRepository = groupRepository;
        }

        public async Task<CommandResult> Handle(AddAnnouncementDto request, CancellationToken cancellationToken)
        {
            var group = groupRepository.GetById(request.GroupId);
            if (group == null)
            {
                throw new Exception("Podana grupa nie istnieje");
            }

            var creator = group.Participients.FirstOrDefault(p => p.Email == request.CreatorEmail);
            if (creator == null)
            {
                throw new Exception("Twórca nie należy do podanej grupy");
            }

            var announcement = AnnouncementAggregate.Create(
                new AnnouncementTitle(request.Title),
                new AnnouncementMessage(request.Message),
                group,
                creator
            );

            await announcemenRepository.Add(announcement);
            await announcemenRepository.SaveChanges();

            return new CommandResult { Result = new OkResult() };
        }
    }
}
