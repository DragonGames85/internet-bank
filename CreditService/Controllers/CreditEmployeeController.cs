using CreditService.Logger;
using CreditService.Model.DTO;
using CreditService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            var startSendRequest = DateTime.Now;
            try
            {
                await _employeeService.CreateNewTariff(model);
                _logger.LogInformation($"Succesful create new tariff");

                var endSendRequest = DateTime.Now;
                var time = endSendRequest - startSendRequest;

                _monitoring.MonitoringService(time, "credit/api/createCreditTariff", "POST", 200, 1, "Succesful create new tariff");
                return Ok();
            }
            catch (ArgumentException e)
            {
                _logger.LogError(e, e.Message);
                var endSendRequest = DateTime.Now;
                var time = endSendRequest - startSendRequest;

                _monitoring.MonitoringService(time, "credit/api/createCreditTariff", "POST", 400, 0, e.Message);
                return Problem(statusCode: 400, title: "Bad request", detail: e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                var endSendRequest = DateTime.Now;
                var time = endSendRequest - startSendRequest;

                _monitoring.MonitoringService(time, "credit/api/createCreditTariff", "POST", 500, 0, e.Message);
                return Problem(statusCode: 500, title: "Something went wrong");
            }
        }
        [HttpGet]
        [Route("getUserCredits/{userId}")]
        public async Task<IActionResult> GetUserCredits(Guid userId)
        {
            var startSendRequest = DateTime.Now;
            try
            {
                var results = await _employeeService.GetUserCredits(userId);
                var endSendRequest = DateTime.Now;
                var time = endSendRequest - startSendRequest;

                _monitoring.MonitoringService(time, "credit/api/getUserCredits/{userId}", "GET", 200, 1, "Succesful get user credits");
                return Ok(results);
            }
            catch (KeyNotFoundException e)
            {
                _logger.LogError(e, e.Message);
                var endSendRequest = DateTime.Now;
                var time = endSendRequest - startSendRequest;

                _monitoring.MonitoringService(time, "credit/api/getUserCredits/{userId}", "GET", 404, 0, e.Message);
                return Problem(statusCode: 404, title: "Not found", detail: e.Message);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                var endSendRequest = DateTime.Now;
                var time = endSendRequest - startSendRequest;

                _monitoring.MonitoringService(time, "credit/api/getUserCredits/{userId}", "GET", 500, 0, e.Message);
                return Problem(statusCode: 500, title: "Something went wrong");
            }
        }
        [HttpGet]
        [Route("getCredit/{creditId}")]
        public async Task<IActionResult> GetUserCredit(Guid creditId)
        {
            var startSendRequest = DateTime.Now;
            try
            {
                var results = await _employeeService.GetCreditDetails(creditId);
                return Ok(results);
            }
            catch (KeyNotFoundException e)
            {
                _logger.LogError(e, e.Message);
                return Problem(statusCode: 404, title: "Not found", detail: e.Message);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Problem(statusCode: 500, title: "Something went wrong");
            }
        }
        [HttpDelete]
        [Route("delete/{creditId}")]
        public async Task<IActionResult> DeleteUserCredit(Guid creditId)
        {
            var startSendRequest = DateTime.Now;
            try
            {
                await _employeeService.DeleteCredit(creditId);
                return Ok();
            }
            catch (KeyNotFoundException e)
            {
                _logger.LogError(e, e.Message);
                return Problem(statusCode: 404, title: "Not found", detail: e.Message);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Problem(statusCode: 500, title: "Something went wrong");
            }
        }
        [HttpGet]
        [Route("rating/{userId}")]
        public async Task<IActionResult> GetOverdueLoans(Guid userId)
        {
            var startSendRequest = DateTime.Now;
            try
            {
                var result = await _employeeService.GetUserCreditRating(userId);
                return Ok(result);
            }
            catch (KeyNotFoundException e)
            {
                // Catch if credits wasn't found in database
                _logger.LogError(e, e.Message);
                return Problem(statusCode: 404, title: e.Message);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Problem(statusCode: 500, title: "Something went wrong");
            }
        }
    }
}
