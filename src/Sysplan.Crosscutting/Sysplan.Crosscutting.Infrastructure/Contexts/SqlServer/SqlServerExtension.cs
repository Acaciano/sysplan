using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Sysplan.Crosscutting.Infrastructure.Contexts.Postgre
{
    public static class SqlServerExtension
    {
        public static void AddSqlServerContext<TContext>(
            this IServiceCollection services,
            IConfiguration configuration) where TContext : DbContext
        {
            var config = new PostgreContextConfig();
            configuration.Bind("SqlServer", config);

            if (string.IsNullOrEmpty(config.ConnectionString))
                throw new Exception("SqlServer connection is empty.");

            services.AddSingleton(config);

            services.AddEntityFrameworkSqlServer()
                .AddDbContext<TContext>(opt =>
                {
                    opt.UseSqlServer(config.ConnectionString);
                });

            services.AddScoped<DbContext, TContext>();
        }
    }
}