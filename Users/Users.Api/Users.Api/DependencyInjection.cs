using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Users.Application.Queries.GetUserByPhoto;
using Users.Database.UserAggregateDatabase;

namespace Users.Api
{
    public static class DependencyInjection
    {
        public static void AddUsersApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMediator, Mediator>();
            services.AddMediatR(typeof(GetUserByPhotoHandler).Assembly);
            services.AddMvcCore().AddApplicationPart(Assembly.Load(new AssemblyName("Users.Api")));
            services.AddDbContext<UserAggregateDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    x => x.MigrationsAssembly("Users.Database")));
            services.AddScoped<IUserAggregateRepository, UserAggregateRepository>();
        }
    }
}
