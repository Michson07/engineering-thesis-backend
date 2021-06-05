using Groups.Application.GroupsCommands;
using Groups.Database;
using Groups.Database.AnnouncementAggregateDatabase;
using Groups.Database.GroupsAggregateDatabase;
using Groups.Database.QuestionAggregateDatabase;
using Groups.Database.ResourceAggregateDatabase;
using Groups.Database.TestAggregateDatabase;
using Groups.Database.TestResultAggregateDatabase;
using MediatR;
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
            services.AddScoped<IMediator, Mediator>();
            services.AddMediatR(typeof(AddGroupHandler).Assembly);
            services.AddMvcCore().AddApplicationPart(Assembly.Load(new AssemblyName("Groups.Api")));
            services.AddDbContext<GroupsDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    x => x.MigrationsAssembly("Groups.Database")));
            services.AddScoped<IGroupAggregateRepository, GroupAggregateRepository>();
            services.AddScoped<IQuestionAggregateRepository, QuestionAggregateRepository>();
            services.AddScoped<ITestAggregateRepository, TestAggregateRepository>();
            services.AddScoped<ITestResultAggregateRepository, TestResultAggregateRepository>();
            services.AddScoped<IAnnouncementAggregateRepository, AnnouncementAggregateRepository>();
            services.AddScoped<IResourceAggregateRepository, ResourceAggregateRepository>();
        }
    }
}
