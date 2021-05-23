using Core.Application;
using MediatR;
using System.Collections.Generic;

namespace Groups.Application.TestResultCommands
{
    public class AddTestResultDto : IRequest<CommandResult>
    {
        public string TestId { get; set; } = string.Empty;
        public string StudentEmail { get; set; } = string.Empty;
        public List<StudentAnswerDto> StudentAnswers { get; set; } = new List<StudentAnswerDto>();
    }

    public class StudentAnswerDto
    {
        public string Question { get; init; } = string.Empty;
        public List<string> ReceivedAnswers { get; init; } = new List<string>();
    }
}
