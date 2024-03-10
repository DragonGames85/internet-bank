using CreditService.Model.Entity;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.EntityFrameworkCore;
namespace CreditService
{
    public class ApplicationDbContext: DbContext
    {

        // Объявляем используемые при создании БД Модели данных
        public DbSet<UserCreditEntity>? Credit { get; set; }
        public DbSet<CreditTariff>? CreditTariff { get; set; }
        public DbSet<LoanPayments> Payments { get; set; }

        // Указывам ключи и индексы атрибутов БД
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserCreditEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<CreditTariff>().HasKey(x => x.Id);
            modelBuilder.Entity<LoanPayments>().HasKey(x => x.Id);
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
