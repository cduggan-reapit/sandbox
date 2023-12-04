using System.Linq.Expressions;
using Sandbox.Api.Data.Models.Entities;

namespace Sandbox.Api.Data.Repositories;

public interface IAddressRepository
{
    /// <summary>
    /// Retrieve a collection of all addresses from the database
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<Address>> GetAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieve a collection of addresses meeting given criteria from the database
    /// </summary>
    /// <param name="criteria"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<Address>> GetFilteredAsync(Expression<Func<Address, bool>> criteria, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Retrieve a single <see cref="Address"/> from the database
    /// </summary>
    /// <param name="id">The Id of the address to return</param>
    /// <param name="cancellationToken"></param>
    /// <returns>The Address corresponding to the given Id value.  Returns null when the Id is not found.</returns>
    Task<Address?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Saves a new Address in the database
    /// </summary>
    /// <param name="entity">The Address to create</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task CreateAsync(Address entity, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Saves a collection of new Addresses in the database
    /// </summary>
    /// <param name="entities">The Addresses to create</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task CreateRangeAsync(IEnumerable<Address> entities, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Saves changes to an Address in the database
    /// </summary>
    /// <param name="entity">The Address to update</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task UpdateAsync(Address entity, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Saves changes to a collection of Addresses in the database
    /// </summary>
    /// <param name="entities">The Addresses to update</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task UpdateRangeAsync(IEnumerable<Address> entities, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Deletes the address matching the given Id from the database
    /// </summary>
    /// <param name="id">The Id of the address to delete</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Deletes the addresses matching the given Ids from the database
    /// </summary>
    /// <param name="ids">The Ids of the addresses to delete</param>
    /// <param name="cancellationToken"></param>
    /// <returns>True if the deletion was attempted.  False if an invalid Id was detected.</returns>
    Task<bool> DeleteRangeAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
}