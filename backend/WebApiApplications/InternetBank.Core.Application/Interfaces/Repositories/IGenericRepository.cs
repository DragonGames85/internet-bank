namespace InternetBank.Core.Application.Interfaces.Repositories;

public interface IGenericRepository<T> where T : class
{
    IQueryable<T> Entities { get; }

    Task<ICollection<T>> GetAll();
    Task<T> GetById(Guid id);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}
