using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using OdenaPWC.Domain;

namespace OdenaPWC.Repository
{
    public class TransportContext : DbContext
    {
        public DbSet<ServiceAlert> ServiceAlerts { get; set; }

       public TransportContext (DbContextOptions options) : base(options) { }

    }
}