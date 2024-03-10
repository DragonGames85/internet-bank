using CreditService.Model.Entity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace CreditService.Repository
{
    public interface ICreditRepository
    {
        Task AddCredit(UserCreditEntity creditEntity);
        Task<List<CreditTariff>> GetAllTariffs();
        Task<CreditTariff> GetCreditTariff(Guid Id);
        Task AddLoanPayment(LoanPayments payment);
        Task<UserCreditEntity> GetCredit(Guid creditId);
        Task<List<LoanPayments>> GetCreditPayments(Guid creditId);
        Task UpdateCreditStatus(Guid creditId);
    }
    public class CreditRepository: ICreditRepository
    {
        private readonly ApplicationDbContext _context;
        public CreditRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddCredit(UserCreditEntity creditEntity)
        {
            await _context.Credit.AddAsync(creditEntity);
            await _context.SaveChangesAsync();
        }
        public async Task<List<CreditTariff>> GetAllTariffs()
        {
           var list = await _context.CreditTariff.ToListAsync();
           return list;
        }
        public async Task<CreditTariff> GetCreditTariff(Guid Id)
        {
            var tariff = await _context.CreditTariff.Where(x => x.Id == Id).FirstOrDefaultAsync();
            return tariff;
        }
        public async Task AddLoanPayment(LoanPayments payment)
        {
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
        }
        public async Task<UserCreditEntity> GetCredit(Guid creditId)
        {
            var list = await _context.Credit.Include(x => x.Tariff).Where(x => x.Id == creditId).FirstOrDefaultAsync();
            return list;
        }
        public async Task<List<LoanPayments>> GetCreditPayments(Guid creditId)
        {
            var list = await _context.Payments.Where(x => (x.CreditId == creditId) && ((x.Status == Model.Enum.PaymentStatusEnum.Waiting) || (x.Status == Model.Enum.PaymentStatusEnum.Overdue))).OrderBy(s => s.NumberPay).ToListAsync();
            return list;
        }
        public async Task UpdateCreditStatus(Guid creditId)
        {
            var credit = await _context.Credit.Include(x => x.Tariff).Where(x => x.Id == creditId).FirstOrDefaultAsync();
            credit.Status = Model.Enum.StatusEnum.Closed;
            await _context.SaveChangesAsync();

        }
    }
}
