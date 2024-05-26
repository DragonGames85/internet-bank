using Azure.Core;
using CreditService.Logger;
using CreditService.Model.DTO;
using CreditService.Services;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
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
        private IExceptionService _exceptionService;
        public UserCreditController(IUserCreditService creditService, ILogger<UserCreditController> logger, IMonitoring monitoring, IExceptionService exceptionService)
        {
            _creditService = creditService;
            _logger = logger;
            _monitoring = monitoring;
            _exceptionService = exceptionService;
        }

        [HttpPost]
        [Route("takeCredit")]
        public async Task<IActionResult> AddUserCredit(CreditModel model)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                await Retry.Do(() => _creditService.AddNewCredit(model), TimeSpan.FromSeconds(1)); 
                _logger.LogInformation($"Succesful create credit");

                stopwatch.Stop();
                TimeSpan executionTime = stopwatch.Elapsed;

                _monitoring.MonitoringService(executionTime, "credit/api/takeCredit", "POST", 200, 1, "Succesful create credit");
                return Ok();
            }
            catch (KeyNotFoundException e)
            {
                // Catch if tarifs was not found in database
                _logger.LogError(e, e.Message);
                stopwatch.Stop();
                TimeSpan executionTime = stopwatch.Elapsed;

                _monitoring.MonitoringService(executionTime, "credit/api/takeCredit", "POST", 404, 0, e.Message);
                return Problem(statusCode: 404, title: "Not found", detail: e.Message);

            }
            catch (ArgumentException e)
            {
                _logger.LogError(e, e.Message);
                stopwatch.Stop();
                TimeSpan executionTime = stopwatch.Elapsed;

                _monitoring.MonitoringService(executionTime, "credit/api/takeCredit", "POST", 400, 0, e.Message);
                return Problem(statusCode: 400, title: "Bad request", detail: e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                stopwatch.Stop();
                TimeSpan executionTime = stopwatch.Elapsed;

                _monitoring.MonitoringService(executionTime, "credit/api/takeCredit", "POST", 500, 0, e.Message);
                return Problem(statusCode: 500, title: "Something went wrong");
            }
        }
        [HttpGet]
        [Route("getTariffs")]
        public async Task<IActionResult> GetAllCreditTariffs()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //bool succesful_request = false;
            //string message = "";
            // for(int i = 0; i < 10; i++)
            //{
            //    try
            //    {
            //        await _exceptionService.GetException();

            //        var result = await _creditService.GetAllCreditTariffs();
            //        succesful_request = true;
            //        _logger.LogInformation($"Succesful get all credit tariffs");
            //        stopwatch.Stop();
            //        TimeSpan executionTime = stopwatch.Elapsed;

            //        _monitoring.MonitoringService(executionTime, "credit/api/getTariffs", "GET", 200, 1, "Succesful get all credit tariffs");
            //        return Ok(result);
            //    }
            //    catch (KeyNotFoundException e)
            //    {
            //        // Catch if tariffs was not found in database
            //        _logger.LogError(e, e.Message);
            //        stopwatch.Stop();
            //        TimeSpan executionTime = stopwatch.Elapsed;

            //        _monitoring.MonitoringService(executionTime, "credit/api/getTariffs", "GET", 404, 0, e.Message);
            //        return Problem(statusCode: 404, title: e.Message);

            //    }
            //    catch (Exception e)
            //    {
            //        _logger.LogInformation("Retry Times " + i.ToString());
            //        message = e.Message + ". Retry Times " + i.ToString();
            //        _logger.LogError(message);

            //        TimeSpan executionTime = stopwatch.Elapsed;
            //        _monitoring.MonitoringService(executionTime, "credit/api/getTariffs", "GET", 500, 0, message);
            //        await Task.Delay(500);
            //    }
            //}
            //if (!succesful_request)
            //{
            //    _logger.LogError( message);

            //    stopwatch.Stop();
            //    TimeSpan executionTime = stopwatch.Elapsed;
            //    _monitoring.MonitoringService(executionTime, "credit/api/getTariffs", "GET", 500, 0, message);

            //    return Problem(statusCode: 500, title: "Something went wrong");
            //}
            try
            {
                var result = await Retry.Do(_creditService.GetAllCreditTariffs, TimeSpan.FromSeconds(1));
                _logger.LogInformation($"Succesful get all credit tariffs");
                  stopwatch.Stop();
                  TimeSpan executionTime = stopwatch.Elapsed;
                 _monitoring.MonitoringService(executionTime, "credit/api/getTariffs", "GET", 200, 1, "Succesful get all credit tariffs");

                return Ok(result);
            }
            catch (KeyNotFoundException e)
            {
                   // Catch if tariffs was not found in database
                    _logger.LogError(e, e.Message);
                    stopwatch.Stop();
                    TimeSpan executionTime = stopwatch.Elapsed;

                    _monitoring.MonitoringService(executionTime, "credit/api/getTariffs", "GET", 404, 0, e.Message);
                    return Problem(statusCode: 404, title: e.Message);

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                stopwatch.Stop();
                TimeSpan executionTime = stopwatch.Elapsed;
               _monitoring.MonitoringService(executionTime, "credit/api/getTariffs", "GET", 500, 0, e.Message);

                return Problem(statusCode: 500, title: "Something went wrong");
            }
            

        }
        [HttpPut]
        [Route("closeCredit/{creditId}")]
        public async Task<IActionResult> CloseCredit(Guid creditId)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                await Retry.Do(() => _creditService.CloseLoan(creditId), TimeSpan.FromSeconds(1));
                _logger.LogInformation($"Succesful get all credit tariffs");

                stopwatch.Stop();
                TimeSpan executionTime = stopwatch.Elapsed;

                _monitoring.MonitoringService(executionTime, "credit/api/closeCredit/{creditId}", "PUT", 200, 1, "Succesful close credit");
                return Ok();
            }
            catch (KeyNotFoundException e)
            {
                // Catch if tariffs was not found in database
                _logger.LogError(e, e.Message);
                stopwatch.Stop();
                TimeSpan executionTime = stopwatch.Elapsed;

                _monitoring.MonitoringService(executionTime, "credit/api/closeCredit/{creditId}", "PUT", 404, 0, e.Message);
                return Problem(statusCode: 404, title: e.Message);

            }
            catch (ArgumentException e)
            {
                _logger.LogError(e, e.Message);

                stopwatch.Stop();
                TimeSpan executionTime = stopwatch.Elapsed;

                _monitoring.MonitoringService(executionTime, "credit/api/closeCredit/{creditId}", "PUT", 400, 0, e.Message);
                return Problem(statusCode: 400, title: e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                stopwatch.Stop();
                TimeSpan executionTime = stopwatch.Elapsed;

                _monitoring.MonitoringService(executionTime, "credit/api/closeCredit/{creditId}", "PUT", 500, 0, e.Message);
                return Problem(statusCode: 500, title: "Something went wrong");
            }
        }
        [HttpGet]
        [Route("overdueLoans/{userId}")]
        public async Task<IActionResult> GetOverdueLoans(Guid userId)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                var result = await Retry.Do(() => _creditService.GetOverdueLoan(userId), TimeSpan.FromSeconds(1));

                stopwatch.Stop();
                TimeSpan executionTime = stopwatch.Elapsed;

                _monitoring.MonitoringService(executionTime, "credit/api/overdueLoans/{userId}", "GET", 200, 1, "Succesful get overdue loans");
                return Ok(result);
            }
            catch (KeyNotFoundException e)
            {
                // Catch if credits wasn't found in database
                _logger.LogError(e, e.Message);

                stopwatch.Stop();
                TimeSpan executionTime = stopwatch.Elapsed;

                _monitoring.MonitoringService(executionTime, "credit/api/overdueLoans/{userId}", "GET", 404, 1, e.Message);
                return Problem(statusCode: 404, title: e.Message);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                stopwatch.Stop();
                TimeSpan executionTime = stopwatch.Elapsed;

                _monitoring.MonitoringService(executionTime, "credit/api/overdueLoans/{userId}", "GET", 500, 1, e.Message);
                return Problem(statusCode: 500, title: "Something went wrong");
            }
        }
        

    }
}
