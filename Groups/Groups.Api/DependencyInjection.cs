using Groups.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Groups.Api
{
    public static class DependencyInjection
    {
        public static void AddGroupsApi(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddScoped<IMediator, Mediator>(); //todo
            //services.AddMediatR(typeof(GetUserByPhotoHandler).Assembly);
            services.AddMvcCore().AddApplicationPart(Assembly.Load(new AssemblyName("Groups.Api")));
            services.AddDbContext<GroupsDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    x => x.MigrationsAssembly("Groups.Database")));
            //services.AddScoped<IUserAggregateRepository, UserAggregateRepository>();
        }
    }
}
