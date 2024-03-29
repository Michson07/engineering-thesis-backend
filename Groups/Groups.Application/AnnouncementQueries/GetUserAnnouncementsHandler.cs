﻿using Core.Application;
using Groups.Database.AnnouncementAggregateDatabase;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Groups.Application.AnnouncementQueries
{
    public class GetUserAnnouncementsHandler : IRequestHandler<GetUserAnnouncementsDto, QueryResult<List<GetUserAnnouncementsView>>>
    {
        private readonly IAnnouncementAggregateRepository announcemenRepository;

        public GetUserAnnouncementsHandler(IAnnouncementAggregateRepository announcemenRepository)
        {
            this.announcemenRepository = announcemenRepository;
        }

        public async Task<QueryResult<List<GetUserAnnouncementsView>>> Handle(GetUserAnnouncementsDto request, CancellationToken cancellationToken)
        {
            var announcements = await announcemenRepository.GetAnnouncementsForUser(request.Email);
            var announcementsView = new List<GetUserAnnouncementsView>();

            foreach (var announcement in announcements)
            {
                announcementsView.Add(new GetUserAnnouncementsView
                {
                    Id = announcement.Id.ToString(),
                    Title = announcement.Title,
                    Message = announcement.Message,
                    Date = announcement.Date,
                    IsCreator = announcement.Creator.Email == request.Email
                });
            }

            var response = new QueryResult<List<GetUserAnnouncementsView>> { BodyResponse = announcementsView };

            return response;
        }
    }
}
