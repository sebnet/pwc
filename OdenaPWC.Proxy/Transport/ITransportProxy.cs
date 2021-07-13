using OdenaPWC.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OdenaPWC.TransportApiProxy
{
    public interface ITransportProxy
    {
        Task<IEnumerable<ServiceAlert>> GetServiceAlertsAsync();
        Task<string> NextTrainArrival(string line, string stop,string destination);
    }

}
