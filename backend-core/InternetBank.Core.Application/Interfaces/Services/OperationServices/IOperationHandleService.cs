using InternetBank.Core.Application.DTOs.OperationDTOs;
using Microsoft.AspNetCore.SignalR;

namespace InternetBank.Core.Application.Interfaces.Services.OperationServices;

public interface IOperationHandleService
{
    Task CreateOperation(CreateOperationDto dto);
}
