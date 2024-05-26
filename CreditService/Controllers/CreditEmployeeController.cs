using CreditService.Logger;
using CreditService.Model.DTO;
using CreditService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CreditService.Controllers
{
    [ApiController]
    [Route("credit/api/")]
    public class CreditEmployeeController : Controller
    {
        private IEmployeeService _employeeService;
        private readonly ILogger<CreditEmployeeController> _logger;
        private IMonitoring _monitoring;
        public CreditEmployeeController(IEmployeeService employeeService, ILogger<CreditEmployeeController> logger, IMonitoring monitoring)
        {
            _employeeService = employeeService;
            _logger = logger;
            _monitoring = monitoring;
        }

        [HttpPost]
        [Route("createCreditTariff")]
        public async Task<IActionResult> CreateNewTariff(AddCreditTariffModel model)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                await Retry.Do(() => _employeeService.CreateNewTariff(model), TimeSpan.FromSeconds(1));
                _logger.LogInformation($"Succesful create new tariff");

                stopwatch.Stop();
                TimeSpan executionTime = stopwatch.Elapsed;

                _monitoring.MonitoringService(executionTime, "credit/api/createCreditTariff", "POST", 200, 1, "Succesful create new tariff");
                return Ok();
            }
            catch (ArgumentException e)
            {
                _logger.LogError(e, e.Message);

                stopwatch.Stop();
                TimeSpan executionTime = stopwatch.Elapsed;

                _monitoring.MonitoringService(executionTime, "credit/api/createCreditTariff", "POST", 400, 0, e.Message);
                return Problem(statusCode: 400, title: "Bad request", detail: e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                stopwatch.Stop();
                TimeSpan executionTime = stopwatch.Elapsed;

                _monitoring.MonitoringService(executionTime, "credit/api/createCreditTariff", "POST", 500, 0, e.Message);
                return Problem(statusCode: 500, title: "Something went wrong");
            }
        }
        [HttpGet]
        [Route("getUserCredits/{userId}")]
        public async Task<IActionResult> GetUserCredits(Guid userId)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                var results = await Retry.Do(() => _employeeService.GetUserCredits(userId), TimeSpan.FromSeconds(1));

                stopwatch.Stop();
                TimeSpan executionTime = stopwatch.Elapsed;

                _monitoring.MonitoringService(executionTime, "credit/api/getUserCredits/{userId}", "GET", 200, 1, "Succesful get user credits");
                return Ok(results);
            }
            catch (KeyNotFoundException e)
            {
                _logger.LogError(e, e.Message);

                stopwatch.Stop();
                TimeSpan executionTime = stopwatch.Elapsed;

                _monitoring.MonitoringService(executionTime, "credit/api/getUserCredits/{userId}", "GET", 404, 0, e.Message);
                return Problem(statusCode: 404, title: "Not found", detail: e.Message);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                stopwatch.Stop();
                TimeSpan executionTime = stopwatch.Elapsed;

                _monitoring.MonitoringService(executionTime, "credit/api/getUserCredits/{userId}", "GET", 500, 0, e.Message);
                return Problem(statusCode: 500, title: "Something went wrong");
            }
        }
        [HttpGet]
        [Route("getCredit/{creditId}")]
        public async Task<IActionResult> GetUserCredit(Guid creditId)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                var results = await Retry.Do(() => _employeeService.GetCreditDetails(creditId), TimeSpan.FromSeconds(1));
                
                stopwatch.Stop();
                TimeSpan executionTime = stopwatch.Elapsed;

                _monitoring.MonitoringService(executionTime, "credit/api/getCredit/{creditId}", "GET", 200, 1, "Succesful get credit details");
                return Ok(results);
            }
            catch (KeyNotFoundException e)
            {
                _logger.LogError(e, e.Message);

                stopwatch.Stop();
                TimeSpan executionTime = stopwatch.Elapsed;

                _monitoring.MonitoringService(executionTime, "credit/api/getCredit/{creditId}", "GET", 404, 0, e.Message);
                return Problem(statusCode: 404, title: "Not found", detail: e.Message);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                stopwatch.Stop();
                TimeSpan executionTime = stopwatch.Elapsed;

                _monitoring.MonitoringService(executionTime, "credit/api/getCredit/{creditId}", "GET", 500, 0, e.Message);
                return Problem(statusCode: 500, title: "Something went wrong");
            }
        }
        [HttpDelete]
        [Route("delete/{creditId}")]
        public async Task<IActionResult> DeleteUserCredit(Guid creditId)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                await Retry.Do(() => _employeeService.DeleteCredit(creditId), TimeSpan.FromSeconds(1));

                stopwatch.Stop();
                TimeSpan executionTime = stopwatch.Elapsed;

                _monitoring.MonitoringService(executionTime, "credit/api/delete/{creditId}", "DELETE", 200, 1, "Delete user credit");
                return Ok();
            }
            catch (KeyNotFoundException e)
            {
                _logger.LogError(e, e.Message);

                stopwatch.Stop();
                TimeSpan executionTime = stopwatch.Elapsed;

                _monitoring.MonitoringService(executionTime, "credit/api/delete/{creditId}", "DELETE", 404, 0, e.Message);
                return Problem(statusCode: 404, title: "Not found", detail: e.Message);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                stopwatch.Stop();
                TimeSpan executionTime = stopwatch.Elapsed;

                _monitoring.MonitoringService(executionTime, "credit/api/delete/{creditId}", "DELETE", 500, 0, e.Message);
                return Problem(statusCode: 500, title: "Something went wrong");
            }
        }
        [HttpGet]
        [Route("rating/{userId}")]
        public async Task<IActionResult> GetOverdueLoans(Guid userId)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                var result = await Retry.Do(() => _employeeService.GetUserCreditRating(userId), TimeSpan.FromSeconds(1));

                stopwatch.Stop();
                TimeSpan executionTime = stopwatch.Elapsed;

                _monitoring.MonitoringService(executionTime, "credit/api/rating/{userId}", "GET", 200, 1, "Get overdue loans");
                return Ok(result);
            }
            catch (KeyNotFoundException e)
            {
                // Catch if credits wasn't found in database
                _logger.LogError(e, e.Message);

                stopwatch.Stop();
                TimeSpan executionTime = stopwatch.Elapsed;

                _monitoring.MonitoringService(executionTime, "credit/api/rating/{userId}", "GET", 404, 0, e.Message);
                return Problem(statusCode: 404, title: e.Message);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                stopwatch.Stop();
                TimeSpan executionTime = stopwatch.Elapsed;

                _monitoring.MonitoringService(executionTime, "credit/api/rating/{userId}", "GET", 404, 0, e.Message);
                return Problem(statusCode: 500, title: "Something went wrong");
            }
        }
    }
}
