namespace InternetBank.Auth.Application.DTOs.UserDTOs;

public record CreateUserDto(
    string Login,
    string Name,
    string Password,
    string Role);
