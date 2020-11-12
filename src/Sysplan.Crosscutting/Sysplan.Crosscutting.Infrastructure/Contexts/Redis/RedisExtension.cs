using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Sysplan.Crosscutting.Infrastructure.Contexts.Redis
{
    public static class RedisExtension
    {
        public static void AddRedisContext(this IServiceCollection services, IConfiguration configuration)
        {
            var config = new RedisContextConfig();

            configuration.Bind("Redis", config);

            if (string.IsNullOrEmpty(config.ConnectionString)) throw new Exception("Redis connection is empty.");

            services.AddSingleton(config);

            services.AddDistributedRedisCache(opt =>
            {
                opt.Configuration = config.ConnectionString;
                opt.InstanceName = config.InstanceName;
            });
        }
    }
}