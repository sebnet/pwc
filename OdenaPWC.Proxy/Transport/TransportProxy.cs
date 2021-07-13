using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OdenaPWC.Common;
using OdenaPWC.Domain;
using Refit;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace OdenaPWC.TransportApiProxy

{
    public class TransportProxy : ITransportProxy
    {
        private readonly ITransportProxyRefit _transportApi;
        private readonly ApiConfiguration _configuration;

        public TransportProxy(ApiConfiguration configuration)
        {
            _configuration = configuration;
            _transportApi = RestService.For<ITransportProxyRefit>(configuration.Uri);
        }

        public async Task<IEnumerable<ServiceAlert>> GetServiceAlertsAsync()
        {
            string serviceResponse = string.Empty;
            try
            {
                serviceResponse = await _transportApi.GetSubteAlertsAsync(_configuration.ApiKey, _configuration.ApiSecret);
                dynamic dynResponse = JsonConvert.DeserializeObject(serviceResponse);
                var entity = dynResponse.entity;
                var alerts = new List<ServiceAlert>();
                if (entity == null) return alerts;
                foreach (var alert in entity)
                {
                    var newAlert = new ServiceAlert();
                    newAlert.Line = alert.informed_entity[0].route_id;
                    newAlert.Message = alert.description_text[0]?.text ?? "";
                    newAlert.Timestamp = DateTime.Now;
                    alerts.Add(newAlert);

                }
                return alerts;


            }
            catch (Exception ex)
            {
                ex.Data.Add("service response", serviceResponse);
                throw;
            }
        }

        public async Task<string> Forecast(string line, string stop, string destination)
        {

            stop = destination == "0" ? stop += "S" : stop += "N";
            string serviceResponse = string.Empty;
            try
            {
                serviceResponse = await _transportApi.GetForecastAsync(_configuration.ApiKey, _configuration.ApiSecret);
                dynamic dynResponse = JsonConvert.DeserializeObject(serviceResponse);
                var entities = dynResponse.Entity;

                foreach (var entity in entities)
                {

                    if (!(entity.Linea.Route_Id == line && entity.Linea.Direction_ID == destination))
                        continue;
                    foreach (var estacion in entity.Linea.Estaciones)
                    {
                        if (estacion.stop_id != stop)
                            continue;
                        return DateTime.Now.AddSeconds(Convert.ToDouble(estacion.departure.delay)).ToString("hh:mm:ss tt");
                    }

                }
                throw new Exception("No se ha encontrado la estación");
            }
            catch (Exception ex)
            {
                ex.Data.Add("service response", serviceResponse);
                throw;
            }
        }
    }
}
