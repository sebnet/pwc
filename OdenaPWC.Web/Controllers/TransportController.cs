using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OdenaPWC.Service;
using System;
using System.Threading.Tasks;

namespace OdenaPWC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransportController : Controller
    {
        private readonly ITransportService _transportService;
        private readonly ILogger<TransportController> _logger;

        public TransportController(ITransportService transportService, ILogger<TransportController> logger)
        {
            _transportService = transportService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("GetAlerts")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAlerts()
        {
            try
            {
                var response = await _transportService.GetServiceAlertsAsync();
                return new OkObjectResult(response);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.ToString(), this);
                return BadRequest();

            }
        }
        [HttpGet("gethistorico/linea/{linea?}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetHistorico(string linea = null, [FromQuery] string from = null, [FromQuery] string to = null)
        {
            try
            {
                DateTime? fromDate = DateTime.TryParse(from, out DateTime parsedFromDate) == true ? parsedFromDate : default(DateTime?);
                DateTime? toDate = DateTime.TryParse(to, out DateTime parsedToDate) ? parsedToDate : default(DateTime?);
                var response = await _transportService.GetHistoricoAsync(linea, fromDate, toDate);
                return new OkObjectResult(response);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.ToString(), this);
                return BadRequest();

            }
        }

        [HttpGet("forecast/line/{line}/stop/{stop}/destination/{destination}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Forecast(string line, string stop, string destination)
        {
            try
            {
                var response = await _transportService.NextTrainArrival(line, stop, destination);
                return new OkObjectResult(response);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.ToString(), this);
                return BadRequest();

            }
        }
    }


}
