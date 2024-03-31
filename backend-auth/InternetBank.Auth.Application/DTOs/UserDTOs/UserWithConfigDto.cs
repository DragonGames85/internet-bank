using InternetBank.Auth.Application.DTOs.ConfigDTOs;

namespace InternetBank.Auth.Application.DTOs.UserDTOs;

public record UserWithConfigDto(
    Guid Id,
    string Name,
    string Role,
    bool IsBanned,
    ConfigDto Config);
