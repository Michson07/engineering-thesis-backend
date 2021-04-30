using Core.Application;
using MediatR;
using System;
using System.Collections.Generic;

namespace Groups.Application.TestCommands
{
    public class AddTestDto : IRequest<CommandResult>
    {
        public string Name { get; set; } = string.Empty;
        public IEnumerable<QuestionDto> Questions { get; set; } = new List<QuestionDto>();
        public string Group { get; set; } = string.Empty;
        public DateTime Date { get; set; } = default!;
        public bool RequirePhoto { get; set; } = false;
        public int? PassedFrom { get; set; }
    }

    public class QuestionDto
    {
        public int Points { get; set; } = 0;
        public string Title { get; set; } = string.Empty;
        public byte[]? Photo { get; set; }
        public IEnumerable<AnswerDto>? Answers { get; set; }
        public bool ClosedQuestion { get; set; } = true;
    }

    public class AnswerDto
    {
        public string Value { get; init; } = string.Empty!;
        public bool Correct { get; set; } = false;
    }
}
