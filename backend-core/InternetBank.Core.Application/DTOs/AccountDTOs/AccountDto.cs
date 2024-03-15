using InternetBank.Core.Application.DTOs.CurrencyDTOs;
using InternetBank.Core.Application.DTOs.UserDTOs;
using InternetBank.Core.Domain.Enumerations;

namespace InternetBank.Core.Application.DTOs.AccountDTOs;

public record AccountDto(
    Guid Id,
    string Number,
    decimal Balance,
    TypeAccount Type, 
    DateTime? CreatedDate,
    DateTime? ClosedDate,
    UserDto User,
    CurrencyDto Currency);
