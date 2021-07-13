using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OdenaPWC.TransportApiProxy
{
    internal interface ITransportProxyRefit
    {
        [Get("/serviceAlerts?json=1&client_id={clientId}&client_secret={clientSecret}")]
        Task<string> GetSubteAlertsAsync(string clientId,string clientSecret);
        [Get("/forecastGTFS?client_id={clientId}&client_secret={clientSecret}")]
        Task<string> GetForecastAsync(string clientId, string clientSecret);
    }
}
