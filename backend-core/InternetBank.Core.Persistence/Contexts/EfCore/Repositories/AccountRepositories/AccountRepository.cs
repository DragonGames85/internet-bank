﻿using InternetBank.Core.Application.Interfaces.Repositories.AccountRepositories;
using InternetBank.Core.Domain.Entities;
using InternetBank.Core.Domain.Enumerations;
using Microsoft.EntityFrameworkCore;

namespace InternetBank.Core.Persistence.Contexts.EfCore.Repositories.AccountRepositories;

public class AccountRepository : IAccountRepository
{
    private readonly ApplicationDbContext _context;

    public AccountRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Account>> GetAccountsIncludedCurrencyByUserId(Guid id)
    {
        return await _context.Accounts
            .Include(e => e.AccountCurrency)
            .Where(e => e.CreatedBy == id)
            .ToListAsync();
    }

    public async Task<IEnumerable<Account>> GetAllAccountsIncludedCurrency()
    {
        return await _context.Accounts
            .Include(e => e.AccountCurrency)
            .ToListAsync();
    }

    public async Task<Account?> GetAccountByNumber(string number)
    {
        return await _context.Accounts
            .Include(e => e.AccountCurrency)
            .FirstOrDefaultAsync(e => e.Number == number);
    }

    public async Task<Account?> GetAccountIncludedCurrencyById(Guid id)
    {
        return await _context.Accounts
            .Include(e => e.AccountCurrency)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Account?> GetMasterAccountWithValue(decimal value)
    {
        return await _context.Accounts
            .Include(e => e.AccountCurrency)
            .FirstOrDefaultAsync(e => e.Type == TypeAccount.Master && e.Balance >= value);
    }
}
