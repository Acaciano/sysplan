using Sysplan.Api.Configurations;
using Sysplan.Crosscutting.Domain.Authentication;
using Sysplan.Crosscutting.Domain.ErrorHandler;
using Sysplan.Crosscutting.Infrastructure.Contexts.MongoDb;
using Sysplan.Crosscutting.Infrastructure.Contexts.Postgre;
using Sysplan.Crosscutting.Infrastructure.Contexts.Redis;
using Sysplan.Infrastructure.Contexts;
using Sysplan.IoC;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace Sysplan.Api
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            string APP_SETTINGS = "appsettings.json";
            string APP_SETTINGS_ENV = $"appsettings.{env.EnvironmentName}.json";

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables();

            Configuration = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile(APP_SETTINGS, optional: true, reloadOnChange: true)
              .AddJsonFile(APP_SETTINGS_ENV, optional: true, reloadOnChange: true)
              .AddEnvironmentVariables()
              .Build();

        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddConexAuthentication(Configuration);
            services.AddAutoMapperSetup();
            services.AddSwaggerSetup();
            services.AddSqlServerContext<SysplanSqlServerContext>(Configuration);
            services.AddMongoDbContext(Configuration);
            services.AddEventStoreContext(Configuration);

            services.AddHealthChecks()
                .AddMongoDb(Configuration.GetSection("MongoDb:ConnectionString").Value, name: "MongoDb Connection")
                .AddMongoDb(Configuration.GetSection("MongoDb:EventStoreConnectionString").Value, name: "EventStore Connection")
                .AddRedis(Configuration.GetSection("Redis:ConnectionString").Value, name: "Redis Connection");

            services.AddHealthChecksUI();
            services.AddControllers();
            services.AddMediatR(typeof(Startup));

            RegisterServices(services);

            string redisConn = Configuration.GetSection("Redis:ConnectionString").Value;
            string instance = Configuration.GetSection("Redis:InstanceName").Value;

            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = redisConn;
                options.InstanceName = instance;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.ConfigureExceptionHandler();

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
            });

            app.UseRouting();

            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void RegisterServices(IServiceCollection services)
        {
            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}