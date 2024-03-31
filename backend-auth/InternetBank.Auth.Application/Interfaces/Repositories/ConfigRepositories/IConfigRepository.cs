using InternetBank.Auth.Domain.Entities;

namespace InternetBank.Auth.Application.Interfaces.Repositories.ConfigRepositories;

public interface IConfigRepository
{
    Task<Config?> GetByUserId(Guid userId);
}
