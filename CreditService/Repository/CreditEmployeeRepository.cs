using CreditService.Model.Entity;
using Microsoft.EntityFrameworkCore;

namespace CreditService.Repository
{
    public interface ICreditEmployeeRepository
    {
        Task AddNewTariff(CreditTariff tariff);
        Task<CreditTariff> FindTariffByName(string Name);
        Task<List<UserCreditEntity>> GetUserCredits(Guid userId);
        Task<UserCreditEntity> GetCredit(Guid creditId);
        Task<List<LoanPayments>> GetCreditPayments(Guid creditId);
    }
    public class CreditEmployeeRepository: ICreditEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        public CreditEmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddNewTariff(CreditTariff tariff)
        {
            await _context.CreditTariff.AddAsync(tariff);
            await _context.SaveChangesAsync();
        }
        public async Task<CreditTariff> FindTariffByName(string Name)
        {
            var tariff = await _context.CreditTariff.Where(x => x.Name == Name).FirstOrDefaultAsync();
            return tariff;
        }
        public async Task<List<UserCreditEntity>> GetUserCredits(Guid userId)
        {
            var list = await _context.Credit.Include(x => x.Tariff).Where(x => x.UserId == userId).ToListAsync();
            return list;
        }
        public async Task<UserCreditEntity> GetCredit(Guid creditId)
        {
            var list = await _context.Credit.Include(x => x.Tariff).Where(x => x.Id == creditId).FirstOrDefaultAsync();
            return list;
        }
        public async Task<List<LoanPayments>> GetCreditPayments(Guid creditId)
        {
            var list = await _context.Payments.Where(x => x.CreditId == creditId).OrderBy(s => s.NumberPay).ToListAsync();
            return list;
        }
    }
}
