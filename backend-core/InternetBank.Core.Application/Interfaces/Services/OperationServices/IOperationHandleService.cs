using InternetBank.Core.Application.DTOs.OperationDTOs;

namespace InternetBank.Core.Application.Interfaces.Services.OperationServices;

public interface IOperationHandleService
{
    Task CreateOperation(CreateOperationDto dto);
}
