using InternetBank.Auth.Application.DTOs.ConfigDTOs;
using InternetBank.Auth.Application.Interfaces.Repositories;
using InternetBank.Auth.Domain.Entities;
using MediatR;

namespace InternetBank.Auth.Application.Features.Settings.Commands;

public class ChangeConfigCommand : IRequest
{
    public Guid UserId { get; set; }
    public ConfigDto Dto { get; set; }
    public ChangeConfigCommand(Guid userId, ConfigDto dto)
    {
        UserId = userId;
        Dto = dto;
    }
}

public class ChangeConfigCommandHandler : IRequestHandler<ChangeConfigCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public ChangeConfigCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(ChangeConfigCommand request, CancellationToken cancellationToken)
    {
        var config = await _unitOfWork.ConfigRepository.GetByUserId(request.UserId)
            ?? throw new Exception($"Config \"{request.UserId}\" is not found.");

        
        config.IsLightTheme = request.Dto.IsLightTheme;

        await _unitOfWork.Repository<Config>().UpdateAsync(config);

        await _unitOfWork.Save(cancellationToken);
    }
}
