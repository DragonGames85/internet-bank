using InternetBank.Auth.Application.DTOs.ConfigDTOs;

namespace InternetBank.Auth.Application.Interfaces.Services.SettingsServices;

public interface ISettingsHandleService
{
    Task ChangeConfig(Guid userId, ConfigDto dto);
    Task AddHideAccount(Guid userId, Guid accountId);
    Task DeleteHideAccount(Guid userId, Guid accountId);
}
