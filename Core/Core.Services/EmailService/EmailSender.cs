using Core.Domain.ValueObjects;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Core.Services.EmailService
{
    public class EmailSender : IEmailSender
    {
        private readonly string senderEmail;
        private readonly string senderPassword;

        public EmailSender(IConfiguration configuration)
        {
            senderEmail = configuration["Gmail:SenderEmail"];
            senderPassword = configuration["Gmail:SenderPassword"];
        }

        public async Task SendAsync(Email email, EmailMessage message)
        {
            var mailMessage = new MailMessage();
            mailMessage.To.Add(email);

            mailMessage.Subject = message.Title;
            mailMessage.Body = message.Message;
            mailMessage.From = new MailAddress(senderEmail);

            using SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(senderEmail, senderPassword),
                EnableSsl = true,
                UseDefaultCredentials = false
            };
            try
            {
                await smtp.SendMailAsync(mailMessage);
            } 
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public async Task SendToMultipleRecipientsAsync(IEnumerable<Email> emails, EmailMessage message)
        {
            foreach(var email in emails)
            {
                await SendAsync(email, message);
            }
        }
    }
}
