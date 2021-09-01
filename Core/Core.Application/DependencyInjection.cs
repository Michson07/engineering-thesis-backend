using Core.Application.Pipeline;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Application
{
    public static class DependencyInjection
    {
        public static void AddCoreApplication(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CatchExceptionsBehavior<,>));
        }
    }
}