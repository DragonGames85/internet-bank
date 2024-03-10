namespace InternetBank.Auth.Application.DTOs.TokenDTOs;

public record TokenDto(
    string Token,
    Guid UserId);
