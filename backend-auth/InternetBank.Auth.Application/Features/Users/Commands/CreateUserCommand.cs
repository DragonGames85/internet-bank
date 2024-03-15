using InternetBank.Auth.Application.DTOs.UserDTOs;
using InternetBank.Auth.Application.Interfaces.Repositories;
using InternetBank.Auth.Domain.Entities;
using MediatR;

namespace InternetBank.Auth.Application.Features.Users.Commands;

public class CreateUserCommand : IRequest
{
    public CreateUserDto Dto { get; set; }
    public CreateUserCommand(CreateUserDto dto)
    {
        Dto = dto;
    }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var role = await _unitOfWork.RoleRepository.GetRoleByName(request.Dto.Role)
            ?? throw new Exception($"Role \"{request.Dto.Role}\" is not found.");

        var user = User.Create(request.Dto.Login, request.Dto.Name, request.Dto.Password);
        user.UserRoles.Add(role);

        await _unitOfWork.Repository<User>().AddAsync(user);

        await _unitOfWork.Save(cancellationToken);
    }
}
