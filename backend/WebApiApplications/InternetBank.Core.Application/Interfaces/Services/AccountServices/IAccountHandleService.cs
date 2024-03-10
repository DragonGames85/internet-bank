using InternetBank.Core.Application.DTOs.AccountDTOs;

namespace InternetBank.Core.Application.Interfaces.Services.AccountServices;

public interface IAccountHandleService
{
    Task CreateAccount(CreateAccountDto dto, Guid userId);
    Task CloseAccount(Guid id);
    Task EditAccount(Guid id, EditAccountDto dto);
    Task DeleteAccount(Guid id);
}
