using CreditService.Logger;
using CreditService.Model.DTO;
using CreditService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace CreditService.Controllers
{

    [ApiController]
    [Route("credit/api/")]
    public class UserCreditController : ControllerBase
    {
        private IUserCreditService _creditService;
        private readonly ILogger<UserCreditController> _logger;
        private IMonitoring _monitoring;
        public UserCreditController(IUserCreditService creditService, ILogger<UserCreditController> logger, IMonitoring monitoring)
        {
            _creditService = creditService;
            _logger = logger;
            _monitoring = monitoring;
        }
        [HttpPost]
        [Route("takeCredit")]
        public async Task<IActionResult> AddUserCredit(CreditModel model)
        {
            var startSendRequest = DateTime.Now;
            try
            {
                await _creditService.AddNewCredit(model);
                _logger.LogInformation($"Succesful create credit");

                var endSendRequest = DateTime.Now;
                var time = endSendRequest - startSendRequest;

                _monitoring.MonitoringService(time, "credit/api/takeCredit", "POST", 200, 1, "Succesful create credit");
                return Ok();
            }
            catch (KeyNotFoundException e)
            {
                // Catch if tarifs was not found in database
                _logger.LogError(e, e.Message);
                var endSendRequest = DateTime.Now;
                var time = endSendRequest - startSendRequest;

                _monitoring.MonitoringService(time, "credit/api/takeCredit", "POST", 404, 0, e.Message);
                return Problem(statusCode: 404, title: "Not found", detail: e.Message);

            }
            catch (ArgumentException e)
            {
                _logger.LogError(e, e.Message);
                var endSendRequest = DateTime.Now;
                var time = endSendRequest - startSendRequest;

                _monitoring.MonitoringService(time, "credit/api/takeCredit", "POST", 400, 0, e.Message);
                return Problem(statusCode: 400, title: "Bad request", detail: e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                var endSendRequest = DateTime.Now;
                var time = endSendRequest - startSendRequest;

                _monitoring.MonitoringService(time, "credit/api/takeCredit", "POST", 500, 0, e.Message);
                return Problem(statusCode: 500, title: "Something went wrong");
            }
        }
        [HttpGet]
        [Route("getTariffs")]
        public async Task<IActionResult> GetAllCreditTariffs()
        {
            var startSendRequest = DateTime.Now;
            try
            {
                var result = await _creditService.GetAllCreditTariffs();
                _logger.LogInformation($"Succesful get all credit tariffs");
                var endSendRequest = DateTime.Now;
                var time = endSendRequest - startSendRequest;

                _monitoring.MonitoringService(time, "credit/api/getTariffs", "GET", 200, 1, "Succesful get all credit tariffs");
                return Ok(result);
            }
            catch (KeyNotFoundException e)
            {
                // Catch if tariffs was not found in database
                _logger.LogError(e, e.Message);
                var endSendRequest = DateTime.UtcNow;
                var time = startSendRequest - endSendRequest;

                _monitoring.MonitoringService(time, "credit/api/getTariffs", "GET", 404, 0, e.Message);
                return Problem(statusCode: 404, title: e.Message);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                var endSendRequest = DateTime.UtcNow;
                var time = startSendRequest - endSendRequest;
                _monitoring.MonitoringService(time, "credit/api/getTariffs", "GET", 500, 0, e.Message);

                return Problem(statusCode: 500, title: "Something went wrong");
            }
           
        }
        [HttpPut]
        [Route("closeCredit/{creditId}")]
        public async Task<IActionResult> CloseCredit(Guid creditId)
        {
            var startSendRequest = DateTime.Now;
            try
            {
                await _creditService.CloseLoan(creditId);
                _logger.LogInformation($"Succesful get all credit tariffs");

                var endSendRequest = DateTime.Now;
                var time = endSendRequest - startSendRequest;

                _monitoring.MonitoringService(time, "credit/api/closeCredit/{creditId}", "PUT", 200, 1, "Succesful close credit");
                return Ok();
            }
            catch (KeyNotFoundException e)
            {
                // Catch if tariffs was not found in database
                _logger.LogError(e, e.Message);
                var endSendRequest = DateTime.Now;
                var time = endSendRequest - startSendRequest;

                _monitoring.MonitoringService(time, "credit/api/closeCredit/{creditId}", "PUT", 404, 0, e.Message);
                return Problem(statusCode: 404, title: e.Message);

            }
            catch (ArgumentException e)
            {
                _logger.LogError(e, e.Message);
                var endSendRequest = DateTime.Now;
                var time = endSendRequest - startSendRequest;
                _monitoring.MonitoringService(time, "credit/api/closeCredit/{creditId}", "PUT", 400, 0, e.Message);
                return Problem(statusCode: 400, title: e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                var endSendRequest = DateTime.Now;
                var time = endSendRequest - startSendRequest;
                _monitoring.MonitoringService(time, "credit/api/closeCredit/{creditId}", "PUT", 500, 0, e.Message);
                return Problem(statusCode: 500, title: "Something went wrong");
            }
        }
        [HttpGet]
        [Route("overdueLoans/{userId}")]
        public async Task<IActionResult> GetOverdueLoans(Guid userId)
        {
            var startSendRequest = DateTime.Now;
            try
            {
                var result = await _creditService.GetOverdueLoan(userId);

                var endSendRequest = DateTime.Now;
                var time = endSendRequest - startSendRequest;

                _monitoring.MonitoringService(time, "credit/api/overdueLoans/{userId}", "GET", 200, 1, "Succesful get overdue loans");
                return Ok(result);
            }
            catch (KeyNotFoundException e)
            {
                // Catch if credits wasn't found in database
                _logger.LogError(e, e.Message);
                var endSendRequest = DateTime.Now;
                var time = endSendRequest - startSendRequest;

                _monitoring.MonitoringService(time, "credit/api/overdueLoans/{userId}", "GET", 404, 1, e.Message);
                return Problem(statusCode: 404, title: e.Message);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                var endSendRequest = DateTime.Now;
                var time = endSendRequest - startSendRequest;

                _monitoring.MonitoringService(time, "credit/api/overdueLoans/{userId}", "GET", 500, 1, e.Message);
                return Problem(statusCode: 500, title: "Something went wrong");
            }
        }
        

    }
}
