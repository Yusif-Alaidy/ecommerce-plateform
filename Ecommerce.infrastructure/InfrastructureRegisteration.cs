using Ecommerce.core.Interfaces;
using Ecommerce.infrastructure.Repositries;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.infrastructure
{
    public static class InfrastructureRegisteration
    {

        public static IServiceCollection InfrastructureConfiguration(this IServiceCollection services)
        {

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            return services;
        }

    }
}
