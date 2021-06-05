using Core.Domain.ValueObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services.EmailService
{
    public interface IEmailSender
    {
        public Task SendAsync(Email email, EmailMessage message);
        public Task SendToMultipleRecipientsAsync(IEnumerable<Email> emails, EmailMessage message);
    }
}
