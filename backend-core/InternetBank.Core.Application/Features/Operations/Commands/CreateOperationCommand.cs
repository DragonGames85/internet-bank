using InternetBank.Core.Application.DTOs.OperationDTOs;
using InternetBank.Core.Application.Interfaces.Repositories;
using InternetBank.Core.Domain.Entities;
using MediatR;

namespace InternetBank.Core.Application.Features.Operations.Commands;

public class CreateOperationCommand : IRequest
{
    public CreateOperationDto Dto { get; set; }
    public bool IsOneWayTransfer { get; set; }
    public bool IsOneWayTransferReceive { get; set; }

    public CreateOperationCommand(CreateOperationDto dto, bool isOneWayTransfer, bool isOneWayTransferReceive)
    {
        Dto = dto;
        IsOneWayTransfer = isOneWayTransfer;
        IsOneWayTransferReceive = isOneWayTransferReceive;
    }
}

public class CreateOperationCommandHandler : IRequestHandler<CreateOperationCommand>
{
    public readonly IUnitOfWork _unitOfWork;

    public CreateOperationCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CreateOperationCommand request, CancellationToken cancellationToken)
    {
        var recieveAccount = request.Dto.ReceiveAccountNumber != null 
            ? await _unitOfWork.AccountRepository.GetAccountByNumber(request.Dto.ReceiveAccountNumber) 
            : null;
        var sendAccount = request.Dto.SendAccountNumber != null 
            ? await _unitOfWork.AccountRepository.GetAccountByNumber(request.Dto.SendAccountNumber) 
            : null;

        var operation = Operation.Create(
            recieveAccount?.Id,
            sendAccount?.Id,
            request.Dto.Name,
            request.Dto.Value,
            request.Dto.Type);
        if (sendAccount != null)
            operation.CreatedBy = sendAccount.CreatedBy; 
        else if (recieveAccount != null)
            operation.CreatedBy = recieveAccount.CreatedBy;

        if (recieveAccount != null)
            operation.ReceiveAccount = recieveAccount;
        if (sendAccount != null)
            operation.SendAccount = sendAccount;

        var isOneWayTransfer = request.IsOneWayTransfer;
        var isOneWayTransferReceive = request.IsOneWayTransferReceive;

        if (isOneWayTransfer)
        {
            if (recieveAccount != null && isOneWayTransferReceive)
                operation.OperationCurrency = recieveAccount.AccountCurrency;
            else if (sendAccount != null && !isOneWayTransferReceive)
                operation.OperationCurrency = sendAccount.AccountCurrency;
        }
        else
        {
            if (recieveAccount != null)
                operation.OperationCurrency = recieveAccount.AccountCurrency;
            else if (sendAccount != null)
                operation.OperationCurrency = sendAccount.AccountCurrency;
        }

        if (sendAccount != null && ((isOneWayTransfer && !isOneWayTransferReceive) || !isOneWayTransfer))
        {
            if (sendAccount.Balance - operation.Value < 0)
                throw new Exception("There is not enough balance in the account.");

            sendAccount.Balance -= operation.Value;
            await _unitOfWork.Repository<Account>().UpdateAsync(sendAccount);
        }
        if (recieveAccount != null && ((isOneWayTransfer && isOneWayTransferReceive) || !isOneWayTransfer))
        {
            recieveAccount.Balance += operation.Value;
            await _unitOfWork.Repository<Account>().UpdateAsync(recieveAccount);
        }

        await _unitOfWork.Repository<Operation>().AddAsync(operation);

        await _unitOfWork.Save(cancellationToken);
    }
}
