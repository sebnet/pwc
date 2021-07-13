using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace OdenaPWC.TransportApiProxy
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddProxies(this IServiceCollection services)
        {
            services.AddTransient<ITransportProxy, TransportProxy>();
            return services;
        }
    }
}
