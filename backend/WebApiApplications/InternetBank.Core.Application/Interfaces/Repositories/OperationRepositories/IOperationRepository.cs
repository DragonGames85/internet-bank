using InternetBank.Core.Domain.Entities;

namespace InternetBank.Core.Application.Interfaces.Repositories.OperationRepositories;

public interface IOperationRepository
{
    Task<IEnumerable<Operation>> GetAllOperationsIncludeAccounts();
    Task<IEnumerable<Operation>> GetOperationsIncludeAccountsByAccountId(Guid accountId);
    Task<IEnumerable<Operation>> GetOperationsIncludeAccountsByUserId(Guid userId);
}
