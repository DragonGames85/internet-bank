using CreditService.Model.DTO;
using CreditService.Model.Entity;
using CreditService.Repository;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace CreditService.Services
{
    public interface IUserCreditService
    {
        Task AddNewCredit(CreditModel model);
        Task<List<CreditTariffModel>> GetAllCreditTariffs();
        Task CloseLoan(Guid creditId);
    }
    public class UserCreditService: IUserCreditService
    {
        private readonly ICreditRepository _creditRepository;
       public UserCreditService(ICreditRepository creditRepository)
        {
            _creditRepository = creditRepository;
        }

        public async Task<List<CreditTariffModel>> GetAllCreditTariffs()
        {
            var list = await _creditRepository.GetAllTariffs();
            if (list == null) throw new KeyNotFoundException("Tariffs not found");
            var tariffsList = new List<CreditTariffModel>();
            foreach (var creditTariff in list)
            {
                var tariff = new CreditTariffModel
                {
                    Id = creditTariff.Id,
                    Name = creditTariff.Name,
                    Percent = creditTariff.Percent,
                    MaxCreditSum = creditTariff.MaxCreditSum,
                    MaxRepaymentPeriod = creditTariff.MaxRepaymentPeriod,
                    MinCreditSum = creditTariff.MinCreditSum,
                    MinRepaymentPeriod  = creditTariff.MinRepaymentPeriod,
                    PennyPercent = creditTariff.PennyPercent,
                    PaymentType = creditTariff.PaymentType,

                };
                tariffsList.Add(tariff);
            }
            return tariffsList;
        }
        public async Task AddNewCredit(CreditModel model)
        {
            var tariff = await _creditRepository.GetCreditTariff(model.TariffId);

            if (tariff == null) throw new KeyNotFoundException("Tariff not found");

            if (tariff.MaxRepaymentPeriod < model.RepaymentPeriod) { throw new ArgumentException("Repayment Period is too large"); }
            if (tariff.MinRepaymentPeriod > model.RepaymentPeriod) { throw new ArgumentException("Repayment Period is too small"); }
            if (tariff.MinCreditSum > model.Value) { throw new ArgumentException("Sum is too small"); }
            if (tariff.MaxCreditSum < model.Value) { throw new ArgumentException("Summ is too large"); }

            var date = DateTime.Now;

                var credit = new UserCreditEntity()
                {
                Status = Model.Enum.StatusEnum.Opened,
                UserId = model.UserId,
                Currency = model.Currency,
                Value = model.Value,
                TariffId = model.TariffId,
                PaymentPeriod = model.PaymentPeriod,
                Tariff = tariff,
                DueDate = date.AddDays(model.RepaymentPeriod),
                RepaymentPeriod = model.RepaymentPeriod,
                };

            await _creditRepository.AddCredit(credit);
            if (tariff.PaymentType == Model.Enum.PaymentTypeEnum.DifferentiatedPayment)
            {
                await CalculationOfPaymentsForDifferentiatedPayments();
            }

            if (tariff.PaymentType == Model.Enum.PaymentTypeEnum.AnnuityPayment)
            {
                await CalculationOfPaymentsForAnnuityDaysPayment(tariff, credit);
            }
        }
        private async Task CalculationOfPaymentsForDifferentiatedPayments()
        {
           
        }
        private async Task CalculationOfPaymentsForAnnuityDaysPayment(CreditTariff tariff, UserCreditEntity credit)
        {
            var i = (double) tariff.Percent / 100;
            var n = credit.RepaymentPeriod / credit.PaymentPeriod;
            var j = (1 + i);
            var K = (i * Math.Pow(j, n)) / (Math.Pow(j, n) - 1);
            var A = K * (double) credit.Value;
            var payment = (decimal) A;


            var numberPay = 1;
            decimal amountOfRepaidPrincipalDebt = 0;
            decimal balanceOwed = credit.Value;
            while (numberPay <= n)
            {
                var percentageForPeriod = (credit.Value - amountOfRepaidPrincipalDebt) * (decimal) i;
                balanceOwed = balanceOwed - (payment - percentageForPeriod);
                var loanPayment = new LoanPayments
                {
                    Date = credit.CreatedAt.AddDays(credit.PaymentPeriod * numberPay),
                    CreditId = credit.Id,
                    AmountOfPayment = payment,
                    Status = Model.Enum.PaymentStatusEnum.Waiting,
                    NumberPay = numberPay,
                    PercentageForPeriod = percentageForPeriod,
                    MainDebt = payment - percentageForPeriod,
                    BalanceOwed = balanceOwed,
                };
                amountOfRepaidPrincipalDebt += (payment - percentageForPeriod);
                await _creditRepository.AddLoanPayment(loanPayment);
                numberPay++;
            }
        }

        public async Task CloseLoan(Guid creditId)
        {
            var credit = await _creditRepository.GetCredit(creditId);
            if (credit == null) { throw new KeyNotFoundException("Credit not found"); };

            var BadPayments = await _creditRepository.GetCreditPayments(creditId);
            if (BadPayments.Count != 0) { throw new ArgumentException("You have not completed all loan payments"); };
            await _creditRepository.UpdateCreditStatus(creditId);

        }
    }

}
