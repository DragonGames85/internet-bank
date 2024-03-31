using InternetBank.Auth.Application.DTOs.ConfigDTOs;
using InternetBank.Auth.Application.DTOs.HideAccountDTOs;
using InternetBank.Auth.Application.DTOs.RoleDTOs;
using InternetBank.Auth.Application.Interfaces.Repositories;
using InternetBank.Auth.Domain.Entities;
using MediatR;

namespace InternetBank.Auth.Application.Features.Settings.Queries;

public class GetHideAccountsQuery : IRequest<List<HideAccountDto>>
{
    public Guid UserId { get; set; }
    public GetHideAccountsQuery(Guid userId)
    {
        UserId = userId;
    }
}

public class GetHideAccountsQueryHandler : IRequestHandler<GetHideAccountsQuery, List<HideAccountDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetHideAccountsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<HideAccountDto>> Handle(GetHideAccountsQuery request, CancellationToken cancellationToken)
    {
        var hideAccounts = await _unitOfWork.HideAccountRepository.GetHideAccountsByUserId(request.UserId);

        var dtoHideAccounts = new List<HideAccountDto>();

        foreach (var hideAccount in hideAccounts)
        {
            dtoHideAccounts.Add(new HideAccountDto(hideAccount.AccountId));
        }

        return dtoHideAccounts;
    }
}
