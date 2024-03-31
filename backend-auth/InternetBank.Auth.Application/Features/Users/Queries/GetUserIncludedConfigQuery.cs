using InternetBank.Auth.Application.DTOs.ConfigDTOs;
using InternetBank.Auth.Application.DTOs.UserDTOs;
using InternetBank.Auth.Application.Interfaces.Repositories;
using InternetBank.Auth.Domain.Entities;
using MediatR;

namespace InternetBank.Auth.Application.Features.Users.Queries;

public class GetUserIncludedConfigQuery : IRequest<UserWithConfigDto?>
{
    public Guid Id { get; set; }

    public GetUserIncludedConfigQuery(Guid id)
    {
        Id = id;
    }
}

public class GetUserIncludedConfigQueryHandler : IRequestHandler<GetUserIncludedConfigQuery, UserWithConfigDto?>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUserIncludedConfigQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<UserWithConfigDto?> Handle(GetUserIncludedConfigQuery request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetUserByIdIncludedConfig(request.Id);

        var dtoConfig = new ConfigDto(user?.UserConfig != null ? user.UserConfig.IsLightTheme : true);

        return user != null 
            ? new UserWithConfigDto(
                user.Id,
                user.Name,
                user.UserRoles?.FirstOrDefault()?.Name ?? "Customer",
                user.IsBanned,
                dtoConfig)
            : null;
    }
}
