using OdenaPWC.Domain;
using OdenaPWC.TransportApiProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdenaPWC.Service
{
    public class TransportService : ITransportService
    {
        private readonly ITransportProxy _transportApi;
        private readonly IUnitOfWork _unitOfWork;

        public TransportService(ITransportProxy transportApi, IUnitOfWork unitOfWork)
        {
            _transportApi = transportApi;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ServiceAlert>> GetHistoricoAsync(string linea, DateTime? from, DateTime? to)
            => await _unitOfWork.ServiceAlertsRepository.GetServiceAlertsAsync(from, to, linea);

        public async Task<IEnumerable<ServiceAlert>> GetServiceAlertsAsync()
        {
            var result = await _transportApi.GetServiceAlertsAsync();
            if (result.Count() > 0)
            {
                await _unitOfWork.ServiceAlertsRepository.AddRangeAsync(result);
                await _unitOfWork.SaveChangesAsync();
            }
            return result;

        }

        public async Task<string> NextTrainArrival(string line, string stop, string destination)
          => await _transportApi.NextTrainArrival(line, stop, destination);
    }
}
