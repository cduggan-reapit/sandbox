using System.Linq.Expressions;
using Sandbox.Api.Data.Entities;

namespace Sandbox.Api.Data.Repositories;

public interface IBaseRepository<T> where T: BaseEntity
{
    /// <summary>
    /// Retrieve a collection of all entities from the database
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieve a collection of entities meeting given criteria from the database
    /// </summary>
    /// <param name="criteria"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<T>> GetFilteredAsync(Expression<Func<T, bool>> criteria, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Retrieve a single entity from the database
    /// </summary>
    /// <param name="id">The Id of the entity to return</param>
    /// <param name="cancellationToken"></param>
    /// <returns>The entity corresponding to the given Id value.  Returns null when the Id is not found.</returns>
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Saves a new entity in the database
    /// </summary>
    /// <param name="entity">The entity to create</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task CreateAsync(T entity, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Saves a collection of new entities in the database
    /// </summary>
    /// <param name="entities">The entities to create</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task CreateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Saves changes to an entity in the database
    /// </summary>
    /// <param name="entity">The entity to update</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Saves changes to a collection of entities in the database
    /// </summary>
    /// <param name="entities">The entities to update</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Deletes the entity matching the given Id from the database
    /// </summary>
    /// <param name="id">The Id of the entity to delete</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Deletes the entities matching the given Ids from the database
    /// </summary>
    /// <param name="ids">The Ids of the entities to delete</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Returns false if an invalid Id was detected.</returns>
    Task<bool> DeleteRangeAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
}