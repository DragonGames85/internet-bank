using InternetBank.Core.Application.DTOs.CurrencyDTOs;
using InternetBank.Core.Application.DTOs.UserDTOs;
using InternetBank.Core.Domain.Enumerations;

namespace InternetBank.Core.Application.DTOs.AccountDTOs;

public record ShortAccountDto(
    Guid Id,
    string Number,
    TypeAccount Type, 
    UserDto User,
    CurrencyDto Currency);
