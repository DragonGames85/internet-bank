using InternetBank.Core.Application.DTOs.CurrencyDTOs;
using InternetBank.Core.Application.Interfaces.Repositories;
using InternetBank.Core.Domain.Entities;
using MediatR;

namespace InternetBank.Core.Application.Features.Currencies.Commands;

public class CreateCurrencyCommand : IRequest
{
    public ShortCurrencyDto Dto { get; set; }
    public CreateCurrencyCommand(ShortCurrencyDto dto)
    {
        Dto = dto;
    }
}

public class CreateCurrencyCommandHandler : IRequestHandler<CreateCurrencyCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateCurrencyCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CreateCurrencyCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.Repository<Currency>().AddAsync(Currency.Create(request.Dto.Name, request.Dto.Symbol));

        await _unitOfWork.Save(cancellationToken);
    }
}
