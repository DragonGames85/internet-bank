namespace InternetBank.Auth.Application.DTOs.UserDTOs;

public record UserDto(
    string Login,
    string Name,
    RoleDto Role,
    bool IsBanned);
