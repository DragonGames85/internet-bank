﻿using InternetBank.Auth.Application.Interfaces.Repositories;
using InternetBank.Auth.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace InternetBank.Auth.Persistence.Contexts.EfCore.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseAuditableEntity
{
    private readonly ApplicationDbContext _context;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IQueryable<T> Entities => _context.Set<T>();

    public async Task<T> AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        return entity;
    }

    public Task DeleteAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
        return Task.CompletedTask;
    }

    public async Task<ICollection<T>> GetAll()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T> GetById(Guid id)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task UpdateAsync(T entity)
    {
        T item = await _context.Set<T>().FindAsync(entity.Id)
            ?? throw new Exception($"{typeof(T).Name} not found.");

        _context.Entry(item).CurrentValues.SetValues(entity);
    }
}
