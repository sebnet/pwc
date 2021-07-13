using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OdenaPWC.Domain.ServiceAlerts
{
    public interface IServiceAlertsRepository : IGenericRepository<ServiceAlert>
    {
        Task<IEnumerable<ServiceAlert>> GetServiceAlertsAsync(DateTime? from, DateTime? to, string linea);
    }
}
