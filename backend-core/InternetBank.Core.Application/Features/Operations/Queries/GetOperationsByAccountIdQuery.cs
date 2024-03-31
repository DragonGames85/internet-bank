using InternetBank.Core.Application.DTOs.AccountDTOs;
using InternetBank.Core.Application.DTOs.CurrencyDTOs;
using InternetBank.Core.Application.DTOs.OperationDTOs;
using InternetBank.Core.Application.DTOs.UserDTOs;
using InternetBank.Core.Application.Interfaces.Repositories;
using MediatR;

namespace InternetBank.Core.Application.Features.Operations.Queries;

public class GetOperationsByAccountIdQuery : IRequest<List<OperationDto>>
{
    public Guid AccountId { get; set; }

    public GetOperationsByAccountIdQuery(Guid accountId)
    {
        AccountId = accountId;
    }
}

public class GetOperationsByAccountIdQueryHandler : IRequestHandler<GetOperationsByAccountIdQuery, List<OperationDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetOperationsByAccountIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<OperationDto>> Handle(GetOperationsByAccountIdQuery request, CancellationToken cancellationToken)
    {
        var operations = await _unitOfWork.OperationRepository.GetOperationsIncludeAccountsByAccountId(request.AccountId);

        var dtoOperations = new List<OperationDto>();
        var dtoUser = new UserDto(request.AccountId, "");

        foreach (var operation in operations)
        {
            var dtoCurrency = new CurrencyDto(operation.OperationCurrency.Id, operation.OperationCurrency.Name, operation.OperationCurrency.Symbol);

            ShortAccountDto? dtoRecieveAccount = null;
            ShortAccountDto? dtoSendAccount = null;

            var dtoRecieveUser = new UserDto(operation?.ReceiveAccount?.CreatedBy ?? Guid.NewGuid(), "");
            var dtoSendUser = new UserDto(operation?.SendAccount?.CreatedBy ?? Guid.NewGuid(), "");

            if (operation?.ReceiveAccount != null)
            {
                dtoRecieveAccount = new ShortAccountDto(
                    operation.ReceiveAccount.Id,
                    operation.ReceiveAccount.Number,
                    operation.ReceiveAccount.Type,
                    dtoRecieveUser,
                    dtoCurrency);
            }
            if (operation?.SendAccount != null)
            {
                dtoSendAccount = new ShortAccountDto(
                    operation.SendAccount.Id,
                    operation.SendAccount.Number,
                    operation.SendAccount.Type,
                    dtoSendUser,
                    dtoCurrency);
            }

            dtoOperations.Add(new OperationDto(
                operation.Id,
                operation.Name,
                operation.Value,
                operation.CreatedDate,
                dtoRecieveAccount,
                dtoSendAccount,
                dtoCurrency));
        }

        return dtoOperations;
    }
}
