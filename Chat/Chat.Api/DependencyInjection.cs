using Chat.Application.GroupConversationCommands;
using Chat.Database;
using Chat.Database.ChatDatabase;
using Chat.Database.GroupChatAggregateDatabase;
using Chat.Database.PrivateChatAggregateDatabase;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Chat.Api
{
    public static class DependencyInjection
    {
        public static void AddChatApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMediator, Mediator>();
            services.AddMediatR(typeof(AddGroupConversationMessageHandler).Assembly);
            services.AddMvcCore().AddApplicationPart(Assembly.Load(new AssemblyName("Chat.Api")));
            services.AddDbContext<ChatDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    x => x.MigrationsAssembly("Chat.Database")));
            services.AddScoped<IGroupChatAggregateRepository, GroupChatAggregateRepository>();
            services.AddScoped<IPrivateChatAggregateRepository, PrivateChatAggregateRepository>();
            services.AddScoped<IChatRepository, ChatRepository>();
        }
    }
}
