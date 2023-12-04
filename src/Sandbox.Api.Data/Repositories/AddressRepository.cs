using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Sandbox.Api.Data.Context;
using Sandbox.Api.Data.Models.Entities;

namespace Sandbox.Api.Data.Repositories;

public class AddressRepository : IAddressRepository
{
    private readonly SandboxDbContext _dbContext;

    /// <summary>
    /// Initialize a new <see cref="AddressRepository"/>
    /// </summary>
    /// <param name="dbContext"></param>
    public AddressRepository(SandboxDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Address>> GetAsync(CancellationToken cancellationToken = default)
        => await _dbContext.Addresses.ToListAsync(cancellationToken);
    
    public async Task<IEnumerable<Address>> GetFilteredAsync(Expression<Func<Address, bool>> criteria, 
        CancellationToken cancellationToken = default)
        => await _dbContext.Addresses.Where(criteria).ToListAsync(cancellationToken);

    public async Task<Address?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _dbContext.Addresses.FindAsync(id, cancellationToken);

    public async Task CreateAsync(Address entity, CancellationToken cancellationToken = default)
    {
        await _dbContext.Addresses.AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
    
    public async Task CreateRangeAsync(IEnumerable<Address> entities, CancellationToken cancellationToken = default)
    {
        await _dbContext.Addresses.AddRangeAsync(entities, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Address entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Addresses.Update(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateRangeAsync(IEnumerable<Address> entities, CancellationToken cancellationToken = default)
    {
        _dbContext.Addresses.UpdateRange(entities);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
    
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.Addresses.FindAsync(id);

        if (entity == null)
            return;
        
        _dbContext.Addresses.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<bool> DeleteRangeAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        var entities = _dbContext.Addresses.Where(a => ids.Contains(a.Id));

        if (await entities.CountAsync(cancellationToken) != ids.Count())
            return false;
        
        _dbContext.Addresses.RemoveRange(entities);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}