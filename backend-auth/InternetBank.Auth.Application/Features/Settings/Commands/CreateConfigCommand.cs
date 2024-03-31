using InternetBank.Auth.Application.DTOs.ConfigDTOs;
using InternetBank.Auth.Application.Interfaces.Repositories;
using InternetBank.Auth.Domain.Entities;
using MediatR;

namespace InternetBank.Auth.Application.Features.Settings.Commands;

public class CreateConfigCommand : IRequest
{
    public Guid UserId { get; set; }
    public CreateConfigCommand(Guid userId)
    {
        UserId = userId;
    }
}

public class CreateConfigCommandHandler : IRequestHandler<CreateConfigCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateConfigCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CreateConfigCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Repository<User>().GetById(request.UserId)
            ?? throw new Exception($"User \"{request.UserId}\" is not found.");

        var config = Config.Create(true);
        config.User = user;

        await _unitOfWork.Repository<Config>().AddAsync(config);

        await _unitOfWork.Save(cancellationToken);
    }
}
