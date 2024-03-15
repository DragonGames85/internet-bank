using InternetBank.Core.Application.DTOs.AccountDTOs;
using InternetBank.Core.Application.DTOs.CurrencyDTOs;
using InternetBank.Core.Application.DTOs.OperationDTOs;
using InternetBank.Core.Application.DTOs.UserDTOs;
using InternetBank.Core.Application.Interfaces.Repositories;
using MediatR;

namespace InternetBank.Core.Application.Features.Operations.Queries;

public class GetOperationsByUserIdQuery : IRequest<List<OperationDto>>
{
    public Guid UserId { get; set; }

    public GetOperationsByUserIdQuery(Guid userId)
    {
        UserId = userId;
    }
}

public class GetOperationsByUserIdQueryHandler : IRequestHandler<GetOperationsByUserIdQuery, List<OperationDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetOperationsByUserIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<OperationDto>> Handle(GetOperationsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var operations = await _unitOfWork.OperationRepository.GetOperationsIncludeAccountsByUserId(request.UserId);

        var dtoOperations = new List<OperationDto>();
        var dtoUser = new UserDto(Guid.NewGuid(), "Benjamin Batton");

        foreach (var operation in operations)
        {
            var dtoCurrency = new CurrencyDto(operation.OperationCurrency.Id, operation.OperationCurrency.Name, operation.OperationCurrency.Symbol);

            ShortAccountDto? dtoRecieveAccount = null;
            ShortAccountDto? dtoSendAccount = null;

            if (operation.ReceiveAccount != null)
            {
                dtoRecieveAccount = new ShortAccountDto(
                    operation.ReceiveAccount.Id,
                    operation.ReceiveAccount.Number,
                    operation.ReceiveAccount.Type,
                    dtoUser,
                    dtoCurrency);
            }
            if (operation.SendAccount != null)
            {
                dtoSendAccount = new ShortAccountDto(
                    operation.SendAccount.Id,
                    operation.SendAccount.Number,
                    operation.SendAccount.Type,
                    dtoUser,
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
