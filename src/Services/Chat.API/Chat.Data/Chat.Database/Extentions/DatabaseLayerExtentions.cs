using Chat.Entities.DatabaseEntities.GPTUser;
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
                    options.UseNpgsql("Host=localhost;Database=GPT;Trusted_Connection=True;", q => q.MigrationsAssembly("Chat.Database"));
                    //options.UseSqlServer(Environment.GetEnvironmentVariable("MssqlDatabaseConnection"), q => q.MigrationsAssembly("Chat.Database"));
                });

            services
                .AddIdentityCore<GPTUser>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            })
                .AddRoles<IdentityRole>()
                .AddTokenProvider<DataProtectorTokenProvider<GPTUser>>("Chat.API")
                .AddEntityFrameworkStores<ChatDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}
