using InternetBank.Core.Application.DTOs.AccountDTOs;
using InternetBank.Core.Application.DTOs.CurrencyDTOs;

namespace InternetBank.Core.Application.DTOs.OperationDTOs;

public record OperationDto(
    Guid Id,
    string Name,
    decimal Value,
    DateTime? CreatedDate,
    ShortAccountDto? ReceiveAccount,
    ShortAccountDto? SendAccount,
    CurrencyDto Currency);
