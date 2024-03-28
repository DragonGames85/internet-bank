using CreditService.Model.DTO;
using CreditService.Services;
using Microsoft.AspNetCore.Mvc;

namespace CreditService.Controllers
{
    [ApiController]
    [Route("credit/api/")]
    public class CreditEmployeeController : Controller
    {
        private IEmployeeService _employeeService;
        private readonly ILogger<CreditEmployeeController> _logger;
        public CreditEmployeeController(IEmployeeService employeeService, ILogger<CreditEmployeeController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        [HttpPost]
        [Route("createCreditTariff")]
        public async Task<IActionResult> CreateNewTariff(AddCreditTariffModel model)
        {
            try
            {
                await _employeeService.CreateNewTariff(model);
                _logger.LogInformation($"Succesful create new tariff");
                return Ok();
            }
            catch (ArgumentException e)
            {
                _logger.LogError(e, e.Message);
                return Problem(statusCode: 400, title: "Bad request", detail: e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Problem(statusCode: 500, title: "Something went wrong");
            }
        }
        [HttpGet]
        [Route("getUserCredits/{userId}")]
        public async Task<IActionResult> GetUserCredits(Guid userId)
        {
            try
            {
                var results = await _employeeService.GetUserCredits(userId);
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
        [HttpGet]
        [Route("getCredit/{creditId}")]
        public async Task<IActionResult> GetUserCredit(Guid creditId)
        {
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
    }
}
