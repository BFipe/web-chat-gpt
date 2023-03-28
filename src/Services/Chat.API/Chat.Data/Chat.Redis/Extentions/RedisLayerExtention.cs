using Chat.Redis.Interfaces;
using Chat.Redis.Repositories;
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
                //q.Configuration = "redis://:bb5fc9f6-bd13-4dfb-859e-a8cfe65d3c81@localhost:6379";
                q.Configuration = Environment.GetEnvironmentVariable("RedisDatabaseConnection");
            });

            services.AddScoped<IRedisDatabaseRepository, RedisDatabaseRepository>();

            return services;
        }
    }
}
