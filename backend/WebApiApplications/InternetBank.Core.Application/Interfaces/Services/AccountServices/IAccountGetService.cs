using InternetBank.Core.Application.DTOs.AccountDTOs;

namespace InternetBank.Core.Application.Interfaces.Services.AccountServices;

public interface IAccountGetService
{
    Task<List<AccountDto>> GetAccounts(Guid userId);
    Task<List<AccountDto>> GetAllAccounts();
}
