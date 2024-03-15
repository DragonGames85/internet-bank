using InternetBank.Auth.Application.DTOs.UserDTOs;
using InternetBank.Auth.Application.Interfaces.Repositories;
using MediatR;

namespace InternetBank.Auth.Application.Features.Currencies.Queries;

public class GetAllUsersQuery : IRequest<List<UserDto>>
{
}

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllUsersQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _unitOfWork.UserRepository.GetAllUsersIncludedRoles();

        var dtoUsers = new List<UserDto>();

        foreach (var user in users)
        {
            dtoUsers.Add(new UserDto(
                user.Id,
                user.Name,
                user.UserRoles?.FirstOrDefault()?.Name ?? "Customer",
                user.IsBanned));
        }

        return dtoUsers;
    }
}
