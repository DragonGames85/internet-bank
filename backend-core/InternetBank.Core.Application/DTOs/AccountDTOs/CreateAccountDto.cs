using InternetBank.Core.Application.DTOs.CurrencyDTOs;
using InternetBank.Core.Application.DTOs.UserDTOs;
using InternetBank.Core.Domain.Enumerations;

namespace InternetBank.Core.Application.DTOs.AccountDTOs;

public record CreateAccountDto(
    TypeAccount Type, 
    string CurrencyName);
