using System.Data;
using CreditService.Repository;

namespace CreditService.Services
{
    public interface IPaymentService
    {
        Task CheckPaymentDay();
    }
    public class PaymentService : IPaymentService
    {
        private readonly ICreditEmployeeRepository _creditEmployeeRepository;
        private readonly ILogger<PaymentService> _logger;
        public PaymentService(ICreditEmployeeRepository creditEmployeeRepository)
        {
            _creditEmployeeRepository = creditEmployeeRepository;
        }
        public async Task CheckPaymentDay()
        {
            var paymentList = await _creditEmployeeRepository.GetPaymentsForDay();
            if (paymentList.Count != 0)
            {
                foreach (var payment in paymentList)
                {
                    if (payment.Status == Model.Enum.PaymentStatusEnum.Waiting)
                    {
                        _logger.LogInformation(payment.Id.ToString());
                    }
                }
            }
        }
    }
}
