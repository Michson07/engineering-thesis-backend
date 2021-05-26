using Core.Application;
using MediatR;
using System.Collections.Generic;

namespace Groups.Application.TestResultCommands
{
    public class UpdateTestResultStatusDto : IRequest<CommandResult>
    {
        public string Email { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string TestId { get; set; } = string.Empty;
        public IEnumerable<QuestionResultDto> Questions { get; set; } = new List<QuestionResultDto>();
    }

    public class QuestionResultDto
    {
        public string Question { get; set; } = string.Empty;
        public int ReceivedPoints { get; set; } = 0;
    }
}
