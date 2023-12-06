using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Sandbox.Api.Data.Context;
using Sandbox.Api.Data.Entities;

namespace Sandbox.Api.Data.Repositories;

public abstract class BaseRepository<T> : IBaseRepository<T> where T: BaseEntity
{
    private readonly SandboxDbContext _dbContext;
    
    protected BaseRepository(SandboxDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _dbContext.Set<T>().ToListAsync(cancellationToken);
    
    public async Task<IEnumerable<T>> GetFilteredAsync(Expression<Func<T, bool>> criteria, 
        CancellationToken cancellationToken = default)
        => await _dbContext.Set<T>().Where(criteria).ToListAsync(cancellationToken);

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _dbContext.Set<T>().FindAsync(id, cancellationToken);

    public async Task CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbContext.Set<T>().AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
    
    public async Task CreateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        await _dbContext.Set<T>().AddRangeAsync(entities, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Set<T>().Update(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        _dbContext.Set<T>().UpdateRange(entities);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
    
    public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Set<T>().Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
    
    public async Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        _dbContext.Set<T>().RemoveRange(entities);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}