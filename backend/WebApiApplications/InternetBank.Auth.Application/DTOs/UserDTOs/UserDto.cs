namespace InternetBank.Auth.Application.DTOs.UserDTOs;

public record UserDto(
    Guid Id,
    string Name,
    string Role,
    bool IsBanned);
