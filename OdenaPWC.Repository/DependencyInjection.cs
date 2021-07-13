using Microsoft.Extensions.DependencyInjection;
using OdenaPWC.Domain;
using OdenaPWC.Domain.ServiceAlerts;
using System;
using System.Collections.Generic;
using System.Text;

namespace OdenaPWC.Repository
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IServiceAlertsRepository, ServiceAlertsRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
