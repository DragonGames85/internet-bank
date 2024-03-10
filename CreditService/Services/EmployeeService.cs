using CreditService.Model.DTO;
using CreditService.Model.Entity;
using CreditService.Repository;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CreditService.Services
{
    public interface IEmployeeService
    {
        Task CreateNewTariff(AddCreditTariffModel model);
        Task<List<UserCreditModel>> GetUserCredits(Guid userId);
        Task<UserCreditDetailsModel> GetCreditDetails(Guid creditId);
    }
    public class EmployeeService: IEmployeeService
    {
        private readonly ICreditEmployeeRepository _creditEmployeeRepository;
        public EmployeeService(ICreditEmployeeRepository creditEmployeeRepository)
        {
            _creditEmployeeRepository = creditEmployeeRepository;
        }
        public async Task CreateNewTariff(AddCreditTariffModel model)
        {
            var nameIsAlreadyExist = await _creditEmployeeRepository.FindTariffByName(model.Name);
            if (nameIsAlreadyExist != null) { throw new ArgumentException("This name already exsist"); }
            var tariffEntity = new CreditTariff
            {
                Name = model.Name,
                Percent = model.Percent,
                MaxCreditSum = model.MaxCreditSum,
                MinCreditSum = model.MinCreditSum,
                MinRepaymentPeriod = model.MinRepaymentPeriod,
                MaxRepaymentPeriod = model.MaxRepaymentPeriod,
                PaymentType = model.PaymentType,
                PennyPercent = model.PennyPercent,
                rateType = model.rateType,
            };
            
            await _creditEmployeeRepository.AddNewTariff(tariffEntity);

        }
        public async Task<List<UserCreditModel>> GetUserCredits(Guid userId)
        {
            var list = await _creditEmployeeRepository.GetUserCredits(userId);
            if (list == null) { throw new KeyNotFoundException("Credits not found"); };

            var creditsList = new List<UserCreditModel>();

            foreach (var credit in list)
            {
                var creditModel = new UserCreditModel
                {
                    Id = credit.Id,
                    Currency = credit.Currency,
                    Status = credit.Status,
                    Value = credit.Value,
                    TariffName = credit.Tariff.Name,
                    RepaymentPeriod = credit.RepaymentPeriod,
                };
                creditsList.Add(creditModel);
            }
            return creditsList;
        }
        public async Task<UserCreditDetailsModel> GetCreditDetails(Guid creditId)
        {
            var credit = await _creditEmployeeRepository.GetCredit(creditId);
            if (credit == null) { throw new KeyNotFoundException("Credit not found"); };
            var tariff = new CreditTariffModel
            {
                Id = credit.Tariff.Id,
                Name = credit.Tariff.Name,
                Percent = credit.Tariff.Percent,
                MaxCreditSum = credit.Tariff.MaxCreditSum,
                MinCreditSum = credit.Tariff.MinCreditSum,
                MaxRepaymentPeriod = credit.Tariff.MaxRepaymentPeriod,
                MinRepaymentPeriod = credit.Tariff.MinRepaymentPeriod,
                PaymentType = credit.Tariff.PaymentType,
                PennyPercent = credit.Tariff.PennyPercent,
            };

            var paymentsList = await _creditEmployeeRepository.GetCreditPayments(creditId);
            var payments = new List<CalculationOfPayments>();
            foreach ( var payment in paymentsList)
            {
                var model = new CalculationOfPayments
                {
                    Date = payment.Date,
                    AmountOfPayment= payment.AmountOfPayment,
                    Status = payment.Status,
                    NumberPay = payment.NumberPay,
                    BalanceOwed= payment.BalanceOwed,
                    PercentageForPeriod= payment.PercentageForPeriod,
                    MainDebt = payment.MainDebt,
                };
                payments.Add(model);
            }
            var creditModel = new UserCreditDetailsModel
            {
                CreditId = credit.Id,
                UserId = credit.UserId,
                Currency = credit.Currency,
                CreatedAt = credit.CreatedAt,
                UpdatedAt = credit.UpdatedAt,
                Status = credit.Status,
                Value = credit.Value,
                Tariff = tariff,
                PaymentPeriod = credit.PaymentPeriod,
                RepaymentPeriod = credit.RepaymentPeriod,
                DueDate = credit.DueDate,
                Payments = payments,
            };
            return creditModel;
        }

    }
}
