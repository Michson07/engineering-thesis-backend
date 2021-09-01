using Core.Api;
using Core.Application;
using Core.Application.Exceptions;
using Groups.Database.AnnouncementAggregateDatabase;
using Groups.Database.GroupsAggregateDatabase;
using Groups.Domain.Aggregates;
using Groups.Domain.ValueObjects;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Groups.Application.AnnouncementCommands
{
    public class AddAnnouncementHandler : IRequestHandler<AddAnnouncementDto, CommandResult>
    {
        private readonly IAnnouncementAggregateRepository announcementRepository;
        private readonly IGroupAggregateRepository groupRepository;

        public AddAnnouncementHandler(IAnnouncementAggregateRepository announcementRepository, IGroupAggregateRepository groupRepository)
        {
            this.announcementRepository = announcementRepository;
            this.groupRepository = groupRepository;
        }

        public async Task<CommandResult> Handle(AddAnnouncementDto request, CancellationToken cancellationToken)
        {
            var group = groupRepository.GetById(request.GroupId);
            if (group == null)
            {
                throw new NotFoundException(request.GroupId, "grupy");
            }

            var creator = group.Participients.FirstOrDefault(p => p.Email == request.CreatorEmail);
            if (creator == null)
            {
                throw new DomainException($"{request.CreatorEmail} nie należy do grupy {group.GroupName}");
            }

            var announcement = AnnouncementAggregate.Create(
                new AnnouncementTitle(request.Title),
                new AnnouncementMessage(request.Message),
                group,
                creator
            );

            await announcementRepository.Add(announcement);
            await announcementRepository.SaveChanges();

            return new CommandResult { Result = new OkResult() };
        }
    }
}
