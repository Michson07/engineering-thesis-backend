using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Users.Api;
using Groups.Api;
using Core.Services.EmailService;
using Notifications.Api;
using Chat.Api;
using Core.Application;
using System.Collections.Generic;
using Groups.Database;
using Microsoft.EntityFrameworkCore;
using Chat.Database;
using Notifications.Database;
using Users.Database.UserAggregateDatabase;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"Bearer token dostêpny w aplikacji w zak³adce Profil",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });

            //Core
            services.AddCoreApplication();

            //Users
            services.AddUsersApi(Configuration);

            //Groups
            services.AddGroupsApi(Configuration);

            //Notifications
            services.AddNotificationsApi(Configuration);

            //Chat
            services.AddChatApi(Configuration);

            //Services
            services.AddEmailService();

            services.BuildServiceProvider().GetService<GroupsDbContext>().Database.Migrate();
            services.BuildServiceProvider().GetService<ChatDbContext>().Database.Migrate();
            services.BuildServiceProvider().GetService<NotificationsDbContext>().Database.Migrate();
            services.BuildServiceProvider().GetService<UserAggregateDbContext>().Database.Migrate();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
