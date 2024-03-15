using InternetBank.Auth.Application.DTOs.UserDTOs;
using InternetBank.Auth.Application.Interfaces.Repositories;
using MediatR;

namespace InternetBank.Auth.Application.Features.Users.Queries;

public class GetUserByCredentialsQuery : IRequest<UserDto?>
{
    public LoginUserDto Dto { get; set; }

    public GetUserByCredentialsQuery(LoginUserDto dto)
    {
        Dto = dto;
    }
}

public class GetUserByCredentialsQueryHandler : IRequestHandler<GetUserByCredentialsQuery, UserDto?>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUserByCredentialsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<UserDto?> Handle(GetUserByCredentialsQuery request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetUserByLoginPasswordIncludedRoles(request.Dto);

        return user != null 
            ? new UserDto(
                user.Id,
                user.Name,
                user.UserRoles?.FirstOrDefault()?.Name ?? "Customer",
                user.IsBanned)
            : null;
    }
}
