using OdenaPWC.Domain.ServiceAlerts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OdenaPWC.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        IServiceAlertsRepository ServiceAlertsRepository { get; }
        Task<int> SaveChangesAsync();
    }
}
