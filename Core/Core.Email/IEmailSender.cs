using Core.Domain.ValueObjects;
using System.Threading.Tasks;

namespace Core.Email
{
    public interface IEmailSender
    {
        public Task SendAsync(Email email, EmailMessage message);
        public Task SendToMultipleRecipientsAsync(Email email, EmailMessage message);
    }
}
