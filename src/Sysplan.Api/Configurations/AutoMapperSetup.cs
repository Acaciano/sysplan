using AutoMapper;
using Sysplan.Application.Mappings;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Sysplan.Api.Configurations
{
    public static class AutoMapperSetup
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper();
            MappingsConfig.RegisterMappings();
        }
    }
}