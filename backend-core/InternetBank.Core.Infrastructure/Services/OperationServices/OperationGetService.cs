using InternetBank.Core.Application.DTOs.OperationDTOs;
using InternetBank.Core.Application.Features.Operations.Queries;
using InternetBank.Core.Application.Interfaces.Services.OperationServices;
using MediatR;

namespace InternetBank.Core.Infrastructure.Services.OperationServices;

public class OperationGetService : IOperationGetService
{
    private readonly IMediator _mediator;

    public OperationGetService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<List<OperationDto>> GetAllOperations()
    {
        var result = await _mediator.Send(new GetAllOperationsQuery());

        return result;
    }

    public async Task<List<OperationDto>> GetOperationsByAccountId(Guid accountId)
    {
        var result = await _mediator.Send(new GetOperationsByAccountIdQuery(accountId));

        return result;
    }

    public async Task<List<OperationDto>> GetOperationsByUserId(Guid userId)
    {
        var result = await _mediator.Send(new GetOperationsByUserIdQuery(userId));

        return result;
    }
}
