using Sysplan.Crosscutting.Bus;
using Sysplan.Crosscutting.Domain.Bus;
using Sysplan.Crosscutting.Domain.Interfaces.Repositories;
using Sysplan.Crosscutting.Domain.Notifications;
using Sysplan.Crosscutting.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Sysplan.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IMediatorHandler, InMemoryBus>();
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<IEventStoreRepository, EventStoreRepository>();

            services.Scan(s => s
               .FromApplicationDependencies(a => a.FullName.StartsWith("Sysplan"))
               .AddClasses().AsMatchingInterface((service, filter) =>
                   filter.Where(i => i.Name.Equals($"I{service.Name}", StringComparison.OrdinalIgnoreCase))).WithScopedLifetime()
               .AddClasses(x => x.AssignableTo(typeof(IMediator))).AsImplementedInterfaces().WithScopedLifetime()
               .AddClasses(x => x.AssignableTo(typeof(IRequestHandler<,>))).AsImplementedInterfaces().WithScopedLifetime()
               .AddClasses(x => x.AssignableTo(typeof(INotificationHandler<>))).AsImplementedInterfaces().WithScopedLifetime()
            );

            services.AddTransient(s => s.GetService<IHttpContextAccessor>().HttpContext?.User);
        }
    }
}