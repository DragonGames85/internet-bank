using InternetBank.Core.Application.DTOs.OperationDTOs;

namespace InternetBank.Core.Application.Interfaces.Services.OperationServices;

public interface IOperationGetService
{
    Task<List<OperationDto>> GetAllOperations();
    Task<List<OperationDto>> GetOperationsByUserId(Guid userId);
    Task<List<OperationDto>> GetOperationsByAccountId(Guid accountId);
}
