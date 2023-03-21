﻿using Chat.Entities.DatabaseEntities.GPTUser;
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
            try
            {
                services.AddDbContext<ChatDbContext>(options =>
                {
                    options.UseSqlServer("Server=localhost;Database=GPT;Trusted_Connection=True;", q => q.MigrationsAssembly("Chat.Database"));
                    //options.UseSqlServer(Environment.GetEnvironmentVariable("DatabaseConnection") ?? "localhost:2222", q => q.MigrationsAssembly("Chat.Database"));
                });

            }
            catch (Exception ex)
            {

                throw;
            }

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
