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
            var principalEmail = GetPrincipalEmail();
            if (principalEmail == null || message.SenderEmail != principalEmail)
            {
                return new NotAllowedResult<string, string>(principalEmail ?? "unknown user", $"add messages as ${message.SenderEmail}");
            }

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
            var principalEmail = GetPrincipalEmail();
            if (principalEmail == null || message.SenderEmail != principalEmail)
            {
                return new NotAllowedResult<string, string>(principalEmail ?? "unknown user", $"add messages as ${message.SenderEmail}");
            }

            var response = await mediator.Send(message);

            return response.Result;
        }

        [HttpGet]
        public async Task<ApiActionResult> GetPrivateConversationAsync(string senderEmail, string recipientEmail)
        {
            var principalEmail = GetPrincipalEmail();
            if (principalEmail == null || (senderEmail != principalEmail && principalEmail != recipientEmail))
            {
                return new NotAllowedResult<string, string>(principalEmail ?? "unknown user", $"${senderEmail} and ${recipientEmail} chat");
            }

            var conversation = new PrivateConversationDto { SenderEmail = senderEmail, RecipientEmail = recipientEmail };
            var response = await mediator.Send(conversation);

            return response;
        }

        [Route("forUser")]
        [HttpGet]
        public async Task<ApiActionResult> GetUserConversationsAsync(string userEmail)
        {
            var principalEmail = GetPrincipalEmail();
            if (principalEmail == null || userEmail != principalEmail)
            {
                return new NotAllowedResult<string, string>(principalEmail ?? "unknown user", $"${userEmail} conversations");
            }

            var response = await mediator.Send(new GetUserConverstationsDto { UserEmail = userEmail });

            return response;
        }
    }
}
