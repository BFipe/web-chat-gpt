using Chat.Database.Extentions;
using Chat.Redis.Extentions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Business.Extentions
{
    public static class BusinessLayerExtentions
    {
        public static IServiceCollection AddBusinessLayerServices(this IServiceCollection services) 
        {
            services.AddDatabaseLayerExtentions();

            services.AddRedisLayerExtentions();

            return services;
        }
    }
}
