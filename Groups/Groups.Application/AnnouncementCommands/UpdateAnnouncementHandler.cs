using Core.Api;
using Core.Application;
using Groups.Database.AnnouncementAggregateDatabase;
using Groups.Domain.ValueObjects;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Groups.Application.AnnouncementCommands
{
    public class UpdateAnnouncementHandler : IRequestHandler<UpdateAnnouncementDto, CommandResult>
    {
        private readonly IAnnouncementAggregateRepository announcemenRepository;

        public UpdateAnnouncementHandler(IAnnouncementAggregateRepository announcemenRepository)
        {
            this.announcemenRepository = announcemenRepository;
        }

        public async Task<CommandResult> Handle(UpdateAnnouncementDto request, CancellationToken cancellationToken)
        {
            var announcement = await announcemenRepository.GetById(request.Id);
            if (announcement == null)
            {
                throw new Exception("Podane ogłoszenie nie istnieje");
            }

            var updatedAnnouncement = announcement.Update(new AnnouncementTitle(request.Title),
                new AnnouncementMessage(request.Message));

            announcemenRepository.Update(updatedAnnouncement);
            await announcemenRepository.SaveChanges();

            return new CommandResult { Result = new OkResult() };
        }
    }
}
