using OdenaPWC.Domain;
using OdenaPWC.Domain.ServiceAlerts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OdenaPWC.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TransportContext _context;
        public IServiceAlertsRepository ServiceAlertsRepository { get; }


        public UnitOfWork(TransportContext transportContext,
            IServiceAlertsRepository serviceAlertsRepository)
        {
            this._context = transportContext;

            this.ServiceAlertsRepository = serviceAlertsRepository;
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
