using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using monitoring_service.Models.DTO;
using monitoring_service.Models.Enum;
using monitoring_service.Services;
using System.Diagnostics;

namespace monitoring_service.Controllers
{
    [ApiController]
    [Route("monitoring/api/")]
    public class MonitoringController : Controller
    {
        private readonly ILogger<MonitoringController> _logger;
        private readonly IMonitoringService _monitoringService;
        public MonitoringController(ILogger<MonitoringController> logger, IMonitoringService monitoringService)
        {
            _logger = logger;
            _monitoringService = monitoringService;
        }
        [HttpPost]
        [Route("tracing")]
        public async Task<IActionResult> ReceivingTracesFromService(TracingDTO model)
        {
            try
            {
                await _monitoringService.CreateTracing(model);
                _logger.LogInformation($"Succesful get traces");
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Problem(statusCode: 500, title: "Something went wrong");
            }
        }
        [HttpGet]
        [Route("all/tracing")]
        public async Task<IActionResult> GetTracing(DateTime begin, DateTime end)
        {
            try
            {
               var result = await _monitoringService.GetAllTracing(begin, end);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Problem(statusCode: 500, title: "Something went wrong");
            }
        }
        [HttpGet]
        [Route("credit/tracing")]
        public async Task<IActionResult> GetCreditTracing(DateTime begin, DateTime end, [FromQuery] TracingType type)
        {
            try
            {
                var result = await _monitoringService.GetCreditTracing(begin, end, type);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Problem(statusCode: 500, title: "Something went wrong");
            }
        }
    }
}
