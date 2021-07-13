using Microsoft.Extensions.DependencyInjection;

namespace OdenaPWC.Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ITransportService, TransportService>();
            return services;
        }
    }
}
