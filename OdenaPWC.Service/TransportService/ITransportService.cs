using OdenaPWC.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OdenaPWC.Service
{
    public interface ITransportService
    {
        Task<IEnumerable<ServiceAlert>> GetServiceAlertsAsync();
        Task<IEnumerable<ServiceAlert>> GetHistoricoAsync(string linea, DateTime? from, DateTime? to);
        Task<string> Forecast(string line, string stop, string destination);
    }
}
