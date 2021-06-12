using System;

namespace Chat.Application
{
    public class MessageView
    {
        public string Text { get; set; } = string.Empty;
        public string User { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}
