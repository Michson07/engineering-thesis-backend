using Core.Api;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Notifications.Api
{
    [Route("api/notifications")]
    public class NotificationsController : BaseController
    {
        private readonly IMediator mediator;

        public NotificationsController(IMediator mediator)
        {
            this.mediator = mediator;
        }
    }
}
