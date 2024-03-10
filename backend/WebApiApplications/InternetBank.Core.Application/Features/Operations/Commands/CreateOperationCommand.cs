using InternetBank.Core.Application.DTOs.OperationDTOs;
using InternetBank.Core.Application.Interfaces.Repositories;
using InternetBank.Core.Domain.Entities;
using MediatR;

namespace InternetBank.Core.Application.Features.Operations.Commands;

public class CreateOperationCommand : IRequest
{
    public CreateOperationDto Dto { get; set; }

    public CreateOperationCommand(CreateOperationDto dto)
    {
        Dto = dto;
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
        var currency = await _unitOfWork.CurrencyRepository.GetCurrencyByName(request.Dto.CurrencyName)
            ?? throw new NullReferenceException("Currency is not found.");

        if ((sendAccount != null && sendAccount.CurrencyId != currency.Id)
            || (recieveAccount != null && recieveAccount.CurrencyId != currency.Id))
        {
            throw new Exception("Accounts and Operation have different currencies.");
        }

        var operation = Operation.Create(
            recieveAccount?.Id,
            sendAccount?.Id,
            request.Dto.Name,
            request.Dto.Value,
            request.Dto.Type);

        if (recieveAccount != null)
            operation.ReceiveAccount = recieveAccount;
        if (sendAccount != null)
            operation.SendAccount = sendAccount;
        operation.OperationCurrency = currency;

        if (sendAccount != null)
        {
            if (sendAccount.Balance - operation.Value < 0)
                throw new Exception("There is not enough balance in the account.");

            sendAccount.Balance -= operation.Value;
            await _unitOfWork.Repository<Account>().UpdateAsync(sendAccount);
        }
        if (recieveAccount != null)
        {
            recieveAccount.Balance += operation.Value;
            await _unitOfWork.Repository<Account>().UpdateAsync(recieveAccount);
        }

        await _unitOfWork.Repository<Operation>().AddAsync(operation);

        await _unitOfWork.Save(cancellationToken);
    }
}
