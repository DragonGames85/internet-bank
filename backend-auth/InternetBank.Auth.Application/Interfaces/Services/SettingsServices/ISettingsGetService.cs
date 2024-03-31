using InternetBank.Auth.Application.DTOs.HideAccountDTOs;

namespace InternetBank.Auth.Application.Interfaces.Services.SettingsServices;

public interface ISettingsGetService
{
    Task<IEnumerable<HideAccountDto>> GetHideAccounts(Guid userId);
}
