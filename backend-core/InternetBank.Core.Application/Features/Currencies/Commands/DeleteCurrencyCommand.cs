using InternetBank.Core.Application.Interfaces.Repositories;
using InternetBank.Core.Domain.Entities;
using MediatR;

namespace InternetBank.Core.Application.Features.Currencies.Commands;

public class DeleteCurrencyCommand : IRequest
{
    public Guid Id { get; set; }
    public DeleteCurrencyCommand(Guid id)
    {
        Id = id;
    }
}

public class DeleteCurrencyCommandHandler : IRequestHandler<DeleteCurrencyCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCurrencyCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteCurrencyCommand request, CancellationToken cancellationToken)
    {
        var currency = await _unitOfWork.Repository<Currency>().GetById(request.Id)
            ?? throw new NullReferenceException("Currency is not found.");

        await _unitOfWork.Repository<Currency>().DeleteAsync(currency);

        await _unitOfWork.Save(cancellationToken);
    }
}
