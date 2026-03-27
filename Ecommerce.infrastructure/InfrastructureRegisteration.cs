using Ecommerce.core.Interfaces;
using Ecommerce.infrastructure.Data;
using Ecommerce.infrastructure.Repositries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.infrastructure
{
    public static class InfrastructureRegisteration
    {

        public static IServiceCollection InfrastructureConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<AppDbContext>(op=>
            {
                op.UseSqlServer(configuration.GetConnectionString("UserDatabase"));
            });
            return services;
        }

    }
}
