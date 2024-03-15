using InternetBank.Core.Application.DTOs.OperationDTOs;
using InternetBank.Core.Application.Features.Operations.Commands;
using InternetBank.Core.Application.Interfaces.Services.OperationServices;
using MediatR;

namespace InternetBank.Core.Infrastructure.Services.OperationServices;

public class OperationHandleService : IOperationHandleService
{
    private readonly IMediator _mediator;

    public OperationHandleService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task CreateOperation(CreateOperationDto dto)
    {
        await _mediator.Send(new CreateOperationCommand(dto));
    }
}
