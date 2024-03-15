namespace InternetBank.Core.Application.DTOs.CurrencyDTOs;

public record CurrencyDto(
    Guid Id,
    string Name,
    string Symbol);
