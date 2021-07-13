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

        /// <summary>
        /// Devuelve histórico de alertas de servicio
        /// </summary>
        /// <param name="linea">la línea para la cual se solicita el histórico</param>
        /// <param name="from">Fecha inicio</param>
        /// <param name="to">Fecha fin</param>
        /// <returns>Alerta de servicios</returns>
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

        /// <summary>
        /// Hora de arrivo del próximo tren
        /// </summary>
        /// <param name="line">línea para la cual se realiza la búsqueda</param>
        /// <param name="stop">la estación para la cual se realiza la búsqueda</param>
        /// <param name="destination">Dirección de destino</param>
        /// <returns></returns>
        public async Task<string> Forecast(string line, string stop, string destination)
          => await _transportApi.Forecast(line, stop, destination);
    }
}
