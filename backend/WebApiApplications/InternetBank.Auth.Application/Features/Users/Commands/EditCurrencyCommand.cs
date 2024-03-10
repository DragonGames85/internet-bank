using InternetBank.Core.Application.DTOs.CurrencyDTOs;
using InternetBank.Core.Application.Interfaces.Repositories;
using InternetBank.Core.Domain.Entities;
using MediatR;

namespace InternetBank.Core.Application.Features.Currencies.Commands;

public class EditCurrencyCommand : IRequest
{
    public Guid Id { get; set; }
    public ShortCurrencyDto Dto { get; set; }
    public EditCurrencyCommand(Guid id, ShortCurrencyDto dto)
    {
        Id = id;
        Dto = dto;
    }
}

public class EditCurrencyCommandHandler : IRequestHandler<EditCurrencyCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public EditCurrencyCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(EditCurrencyCommand request, CancellationToken cancellationToken)
    {
        var currency = await _unitOfWork.Repository<Currency>().GetById(request.Id);

        if (request.Dto.Name != null)
            currency.Name = request.Dto.Name;
        if (request.Dto.Symbol != null)
            currency.Symbol = request.Dto.Symbol;

        await _unitOfWork.Repository<Currency>().UpdateAsync(currency);

        await _unitOfWork.Save(cancellationToken);
    }
}
