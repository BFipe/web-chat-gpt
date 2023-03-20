using Chat.Entities.DatabaseEntities.GPTUser;
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
                options.UseSqlServer(Environment.GetEnvironmentVariable("DatabaseConnection"), q => q.MigrationsAssembly("Chat.Database"));
            });

            services.AddIdentityCore<GPTUser>
            return services;
        }
    }
}
