using Chat.Application.GroupConversationCommands;
using Chat.Application.GroupConversationQueries;
using Chat.Application.PrivateConversationCommands;
using Chat.Application.PrivateConversationQueries;
using Chat.Application.UserConversationsQueries;
using Core.Api;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Chat.Api
{
    [Route("api/chat")]
    public class ChatController : BaseController
    {
        private readonly IMediator mediator;

        public ChatController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Route("group")]
        [HttpPost]
        public async Task<ApiActionResult> AddGroupMessageAsync(AddGroupConversationMessageDto message)
        {
            var response = await mediator.Send(message);

            return response.Result;
        }

        [Route("group")]
        [HttpGet]
        public async Task<ApiActionResult> GetGroupConversationAsync(string groupId)
        {
            var response = await mediator.Send(new GroupConversationDto { GroupId = groupId });

            return response;
        }

        [HttpPost]
        public async Task<ApiActionResult> AddPrivateMessageAsync(AddPrivateConversationMessageDto message)
        {
            var response = await mediator.Send(message);

            return response.Result;
        }

        [HttpGet]
        public async Task<ApiActionResult> GetPrivateConversationAsync(string senderEmail, string recipientEmail)
        {
            var conversation = new PrivateConversationDto { SenderEmail = senderEmail, RecipientEmail = recipientEmail };
            var response = await mediator.Send(conversation);

            return response;
        }

        [Route("forUser")]
        [HttpGet]
        public async Task<ApiActionResult> GetUserConversationsAsync(string userEmail)
        {
            var response = await mediator.Send(new GetUserConverstationsDto { UserEmail = userEmail });

            return response;
        }
    }
}
