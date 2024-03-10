using CreditService.Model.DTO;
using CreditService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CreditService.Controllers
{
    
    [ApiController]
    [Route("api/")]
    public class UserCreditController : ControllerBase
    {
        private IUserCreditService _creditService;
        private readonly ILogger<UserCreditController> _logger;
        public UserCreditController(IUserCreditService creditService, ILogger<UserCreditController> logger)
        {
            _creditService = creditService;
            _logger = logger;
        }
        [HttpPost]
        [Route("takeCredit")]
        public async Task<IActionResult> AddUserCredit(CreditModel model)
        {
            try
            {
                await _creditService.AddNewCredit(model);
                _logger.LogInformation($"Succesful create credut");

                return Ok();
            }
            catch (KeyNotFoundException e)
            {
                // Catch if tarifs was not found in database
                _logger.LogError(e, e.Message);
                return Problem(statusCode: 404, title: "Not found", detail: e.Message);

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
        [Route("getTariffs")]
        public async Task<IActionResult> GetAllCreditTariffs()
        {
            try
            {
                var result = await _creditService.GetAllCreditTariffs();
                _logger.LogInformation($"Succesful get all credit tariffs");
                return Ok(result);
            }
            catch (KeyNotFoundException e)
            {
                // Catch if tariffs was not found in database
                _logger.LogError(e, e.Message);
                return Problem(statusCode: 404, title: e.Message);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Problem(statusCode: 500, title: "Something went wrong");
            }
        }
        [HttpPut]
        [Route("closeCredit/{creditId}")]
        public async Task<IActionResult> CloseCredit(Guid creditId)
        {
            try
            {
                 await _creditService.CloseLoan(creditId);
                _logger.LogInformation($"Succesful get all credit tariffs");
                return Ok();
            }
            catch (KeyNotFoundException e)
            {
                // Catch if tariffs was not found in database
                _logger.LogError(e, e.Message);
                return Problem(statusCode: 404, title: e.Message);

            }
            catch(ArgumentException e)
            {
                _logger.LogError(e, e.Message);
                return Problem(statusCode: 400, title: e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Problem(statusCode: 500, title: "Something went wrong");
            }
        }
    }
}
