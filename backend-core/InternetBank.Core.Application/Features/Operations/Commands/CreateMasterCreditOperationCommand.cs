using InternetBank.Core.Application.DTOs.OperationDTOs;
using InternetBank.Core.Application.Interfaces.Repositories;
using InternetBank.Core.Domain.Entities;
using MediatR;

namespace InternetBank.Core.Application.Features.Operations.Commands;

public class CreateMasterCreditOperationCommand : IRequest
{
    public CreateOperationDto Dto { get; set; }

    public CreateMasterCreditOperationCommand(CreateOperationDto dto)
    {
        Dto = dto;
    }
}

public class CreateMasterCreditOperationCommandHandler : IRequestHandler<CreateMasterCreditOperationCommand>
{
    public readonly IUnitOfWork _unitOfWork;

    public CreateMasterCreditOperationCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CreateMasterCreditOperationCommand request, CancellationToken cancellationToken)
    {
        var recieveAccount = request.Dto.ReceiveAccountNumber != null 
            ? await _unitOfWork.AccountRepository.GetAccountByNumber(request.Dto.ReceiveAccountNumber) 
            : null;
        var sendAccount = await _unitOfWork.AccountRepository.GetMasterAccountWithValue(request.Dto.Value)
            ?? throw new Exception("There isn't balance in master accounts.");

        var operation = Operation.Create(
            null,
            sendAccount?.Id,
            request.Dto.Name,
            request.Dto.Value,
            request.Dto.Type);

        if (sendAccount != null)
        {
            operation.CreatedBy = sendAccount.CreatedBy;
            operation.ReceiveAccount = sendAccount;
            operation.OperationCurrency = sendAccount.AccountCurrency;

            if (sendAccount.Balance - operation.Value < 0)
                throw new Exception("There is not enough balance in the account.");

            sendAccount.Balance -= operation.Value;
            await _unitOfWork.Repository<Account>().UpdateAsync(sendAccount);
        }

        await _unitOfWork.Repository<Operation>().AddAsync(operation);

        await _unitOfWork.Save(cancellationToken);
    }
}
