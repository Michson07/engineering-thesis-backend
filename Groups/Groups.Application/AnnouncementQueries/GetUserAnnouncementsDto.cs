using Core.Application;
using MediatR;
using System.Collections.Generic;

namespace Groups.Application.AnnouncementQueries
{
    public class GetUserAnnouncementsDto : IRequest<QueryResult<List<GetUserAnnouncementsView>>>
    {
        public string UserId { get; set; } = string.Empty;
    }
}
