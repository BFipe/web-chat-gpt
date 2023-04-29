using Chat.Database.Interfaces;
using Chat.Database.Repositories;
using Chat.Entities.DatabaseEntities.GPTUser;
using Chat.Entities.GPTEntities.APIKey;
using Chat.Entities.GPTEntities.Chat;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Database.Extentions
{
    public static class DatabaseLayerExtentions
    {
        public static IServiceCollection AddDatabaseLayerExtentions(this IServiceCollection services)
        {
            services.AddDbContext<ChatDbContext>(options =>
            {
                //options.UseNpgsql("Server=127.0.0.1,5432;Database=GPTChat;User Id=ilya.maximov11@gmail.com;Password=bb5fc9f6-bd13-4dfb-859e-a8cfe65d3c81;", q => q.MigrationsAssembly("Chat.Database"));
                options.UseNpgsql(Environment.GetEnvironmentVariable("PostgresDatabaseConnection"), q => q.MigrationsAssembly("Chat.Database"));
            });

            services.AddDataProtection();


            services
                .AddIdentityCore<GPTUser>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            })
                .AddRoles<IdentityRole>()
                .AddTokenProvider<DataProtectorTokenProvider<GPTUser>>(Environment.GetEnvironmentVariable("JWTLoginProvider"))
                .AddEntityFrameworkStores<ChatDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IGPTBaseRepository<APIKey>, GPTBaseRepository<APIKey>>();
            services.AddScoped<IGPTBaseRepository<ChatHistory>, GPTBaseRepository<ChatHistory>>();
            services.AddScoped<IGPTBaseRepository<GPTUser>, GPTBaseRepository<GPTUser>>();

            return services;
        }
    }
}
