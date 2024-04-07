using InternetBank.Core.Domain.Enumerations;

namespace InternetBank.Core.Application.DTOs.OperationDTOs;

public record CreateOperationDto(
    string Name,
    decimal Value,
    string? ReceiveAccountNumber,
    string? SendAccountNumber,
    TypeOperation Type);
