using Microsoft.Extensions.DependencyInjection;

namespace Core.Services.EmailService
{
    public static class DependencyInjection
    {
        public static void AddEmailService(this IServiceCollection services)
        {
            services.AddScoped<IEmailSender, EmailSender>();
        }
    }
}
