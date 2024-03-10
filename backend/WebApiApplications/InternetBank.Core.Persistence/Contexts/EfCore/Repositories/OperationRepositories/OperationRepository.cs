using InternetBank.Core.Application.Interfaces.Repositories.OperationRepositories;
using InternetBank.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace InternetBank.Core.Persistence.Contexts.EfCore.Repositories.OperationRepositories;

public class OperationRepository : IOperationRepository
{
    private readonly ApplicationDbContext _context;

    public OperationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Operation>> GetAllOperationsIncludeAccounts()
    {
        return await _context.Operations
            .Include(e => e.ReceiveAccount)
            .Include(e => e.SendAccount)
            .Include(e => e.OperationCurrency)
            .ToListAsync();
    }

    public async Task<IEnumerable<Operation>> GetOperationsIncludeAccountsByAccountId(Guid accountId)
    {
        return await _context.Operations
            .Where(e => e.SendAccountId == accountId || e.ReceiveAccountId == accountId)
            .Include(e => e.ReceiveAccount)
            .Include(e => e.SendAccount)
            .Include(e => e.OperationCurrency)
            .ToListAsync();
    }

    public async Task<IEnumerable<Operation>> GetOperationsIncludeAccountsByUserId(Guid userId)
    {
        return await _context.Operations
            .Where(e => e.CreatedBy == userId)
            .Include(e => e.ReceiveAccount)
            .Include(e => e.SendAccount)
            .Include(e => e.OperationCurrency)
            .ToListAsync();
    }
}
