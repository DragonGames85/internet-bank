using InternetBank.Core.Application.DTOs.OperationDTOs;
using InternetBank.Core.Application.Features.Accounts.Queries;
using InternetBank.Core.Application.Features.Operations.Commands;
using InternetBank.Core.Application.Interfaces.Services.OperationServices;
using InternetBank.Core.Domain.Enumerations;
using InternetBank.Core.Infrastructure.Refit.Interfaces.Cbr;
using MediatR;
using System;

namespace InternetBank.Core.Infrastructure.Services.OperationServices;

public class OperationHandleService : IOperationHandleService
{
    private readonly IMediator _mediator;
    private readonly IOperationNotificationService _notificationService;
    private readonly ICbrClient _cbrClient;

    public OperationHandleService(IMediator mediator, IOperationNotificationService notificationService, ICbrClient cbrClient)
    {
        _mediator = mediator;
        _notificationService = notificationService;
        _cbrClient = cbrClient;
    }

    public async Task CreateOperation(CreateOperationDto dto, bool isCreditOperation = false)
    {
        if (isCreditOperation)
        {
            if (!(dto.ReceiveAccountNumber != null && dto.SendAccountNumber == null))
                throw new Exception("Incorrect credit operation.");

            var currencyConvert = await _cbrClient.GetCurrencyConvert()
                ?? throw new Exception("Couldn't get the exchange rate.");
            currencyConvert.Rates.Add("RUB", 1);

            var clientAccount = await _mediator.Send(new GetAccountByNumberQuery(dto.ReceiveAccountNumber));

            var clientValue = dto.Value;
            var creditValueInRub = clientValue / currencyConvert.Rates[clientAccount.Currency.Name];

            await _mediator.Send(new CreateMasterCreditOperationCommand(new CreateOperationDto("Снятие с мастер счёта", creditValueInRub, dto.ReceiveAccountNumber, null, dto.Type)));
            await _mediator.Send(new CreateOperationCommand(dto, false, false));

            return;
        }

        if (dto.SendAccountNumber != null && dto.ReceiveAccountNumber != null && await _mediator.Send(new IsAccountsWithDifferentCurrenciesQuery(dto.SendAccountNumber, dto.ReceiveAccountNumber)))
        {
            var currencyConvert = await _cbrClient.GetCurrencyConvert()
                ?? throw new Exception("Couldn't get the exchange rate.");
            currencyConvert.Rates.Add("RUB", 1);

            var firstAccount = await _mediator.Send(new GetAccountByNumberQuery(dto.SendAccountNumber));
            var secondAccount = await _mediator.Send(new GetAccountByNumberQuery(dto.ReceiveAccountNumber));

            var firstValue = dto.Value;
            var valueInRub = firstValue / currencyConvert.Rates[firstAccount.Currency.Name];
            var secondValue = valueInRub * currencyConvert.Rates[secondAccount.Currency.Name];

            await _mediator.Send(new CreateOperationCommand(new CreateOperationDto(dto.Name, firstValue, dto.ReceiveAccountNumber, dto.SendAccountNumber, dto.Type), true, false));
            await _mediator.Send(new CreateOperationCommand(new CreateOperationDto(dto.Name, secondValue, dto.ReceiveAccountNumber, dto.SendAccountNumber, dto.Type), true, true));
        }
        else
        {
            await _mediator.Send(new CreateOperationCommand(dto, false, false));
        }


        await _notificationService.NotifyAllClientsAsync("refreshOperationData");
    }
}
