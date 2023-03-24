using Chat.GPT.Interfaces;
using Chat.GPT.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Chat.GPT.Extentions
{
    public static class GPTLayerExtention
    {
        public static IServiceCollection AddGPTLayerExtentions(this IServiceCollection services)
        {
            services.AddScoped<IChatGPTRepository, ChatGPTRepository>();

            return services;
        }
    }
}
