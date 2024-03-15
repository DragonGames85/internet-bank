using InternetBank.Auth.Application.DTOs.UserDTOs;
using InternetBank.Auth.Application.Interfaces.Repositories;
using InternetBank.Auth.Domain.Entities;
using MediatR;

namespace InternetBank.Auth.Application.Features.Currencies.Queries;

public class GetUserQuery : IRequest<UserDto>
{
    public Guid Id { get; set; }

    public GetUserQuery(Guid id)
    {
        Id = id;
    }
}

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUserQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Repository<User>().GetById(request.Id);

        return new UserDto(
            user.Id,
            user.Name,
            user.UserRoles?.FirstOrDefault()?.Name ?? "Customer",
            user.IsBanned);
    }
}
