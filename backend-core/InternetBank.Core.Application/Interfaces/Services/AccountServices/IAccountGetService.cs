using InternetBank.Core.Application.DTOs.AccountDTOs;

namespace InternetBank.Core.Application.Interfaces.Services.AccountServices;

public interface IAccountGetService
{
    Task<List<AccountDto>> GetAccounts(Guid userId, string name = "");
    Task<List<AccountDto>> GetAllAccounts();
    Task<AccountDto> GetAccount(Guid id);
}
