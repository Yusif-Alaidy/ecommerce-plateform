using Ecommerce.core.Interfaces;
using Ecommerce.core.Service_Interfaces;
using Ecommerce.infrastructure.Data;
using Ecommerce.infrastructure.Repositries;
using Ecommerce.infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
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
            services.AddSingleton<IImageManagementService,ImageManagementService>();
            services.AddSingleton<IFileProvider>(
                new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"wwwroot")));
            services.AddDbContext<AppDbContext>(op=>
            {
                op.UseSqlServer(configuration.GetConnectionString("UserDatabase"));
            });
            
            return services;
        }

    }
}
