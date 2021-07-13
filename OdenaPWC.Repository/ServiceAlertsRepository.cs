using Microsoft.EntityFrameworkCore;
using OdenaPWC.Domain;
using OdenaPWC.Domain.ServiceAlerts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdenaPWC.Repository
{
    public class ServiceAlertsRepository : GenericRepository<ServiceAlert>, IServiceAlertsRepository
    {
        public ServiceAlertsRepository(TransportContext context) : base(context)
        {

        }

        public async Task<IEnumerable<ServiceAlert>> GetServiceAlertsAsync(DateTime? from, DateTime? to, string line)
        {
            return await _context.ServiceAlerts
            .Where(x =>
                (from == null || x.Timestamp > from) &&
                (to == null || x.Timestamp <= to) &&
                (line == null || x.Line == line)).ToListAsync();

        }



    }
}
