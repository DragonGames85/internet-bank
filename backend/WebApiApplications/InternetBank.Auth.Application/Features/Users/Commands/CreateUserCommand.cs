using InternetBank.Auth.Application.DTOs.TokenDTOs;
using InternetBank.Auth.Application.DTOs.UserDTOs;
using InternetBank.Auth.Application.Interfaces.Repositories;
using InternetBank.Auth.Domain.Entities;
using MediatR;

namespace InternetBank.Auth.Application.Features.Currencies.Commands;

public class CreateUserCommand : IRequest<TokenDto>
{
    public CreateUserDto Dto { get; set; }
    public CreateUserCommand(CreateUserDto dto)
    {
        Dto = dto;
    }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, TokenDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TokenDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.Repository<User>().AddAsync(User.Create(request.Dto.Login, request.Dto.Name, request.Dto.Password));

        await _unitOfWork.Save(cancellationToken);
    }
}
