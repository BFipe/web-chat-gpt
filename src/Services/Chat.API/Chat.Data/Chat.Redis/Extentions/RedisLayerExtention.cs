using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Redis.Extentions
{
    public static class RedisLayerExtention
    {
        public static IServiceCollection AddRedisLayerExtentions(this IServiceCollection services)
        {
            services.AddStackExchangeRedisCache(q =>
            {
                q.Configuration = "";
                q.Configuration = Environment.GetEnvironmentVariable("RedisDatabaseConnection");
            });

            return services;
        }
    }
}
