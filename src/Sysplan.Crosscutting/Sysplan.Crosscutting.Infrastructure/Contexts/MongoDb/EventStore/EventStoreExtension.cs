using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Net.Sockets;

namespace Sysplan.Crosscutting.Infrastructure.Contexts.MongoDb
{
    public static class EventStoreExtension
    {
        public static void AddEventStoreContext(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var config = new EventStoreContextConfig();

            configuration.Bind("MongoDb", config);

            if (string.IsNullOrEmpty(config.EventStoreConnectionString)) throw new Exception("EventStore connection is empty.");

            services.AddSingleton(config);
            services.AddSingleton(t =>
            {
                var settings = MongoClientSettings.FromConnectionString(config.EventStoreConnectionString);

                void SocketConfigurator(Socket s) => s.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);

                settings.SocketTimeout = TimeSpan.FromMinutes(5);
                settings.ConnectTimeout = TimeSpan.FromSeconds(60);
                settings.MaxConnectionIdleTime = TimeSpan.FromSeconds(30);
                settings.ClusterConfigurator = builder => builder
                    .ConfigureTcp(tcp => tcp.With(socketConfigurator: (Action<Socket>)SocketConfigurator));

                return new MongoClient(settings);
            });

            services.AddScoped<IEventStoreContext, EventStoreContext>();
        }
    }
}