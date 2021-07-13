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

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                await _transportService.GetServiceAlertsAsync();
                return new OkResult();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex,ex.ToString(),this);
                throw;
            }
        }
    }

    
}
