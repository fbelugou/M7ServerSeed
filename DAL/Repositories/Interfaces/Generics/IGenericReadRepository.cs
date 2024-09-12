using Domain.Entities;

namespace DAL.Repositories.Interfaces.Generics;
public interface IGenericReadRepository<TKey, TEntity> where TEntity : Entity
{
    /// <summary>
    /// Retrieve an entity from the database.
    /// </summary>
    /// <param name="id">
    /// Unique identifier of the entity to retrieve.
    /// </param>
    /// <returns>
    ///  Entity retrieved.
    /// </returns>
    /// <exception cref="Domain.Exceptions.NotFoundEntityException{TEntity}">
    ///  Entity with the given identifier is not found.
    /// </exception>
    public Task<TEntity> GetAsync(TKey id);

    /// <summary>
    /// Retrieve all entities from the database.
    /// </summary>
    /// <returns>
    ///  Entities retrieved.
    /// </returns>
    public Task<IEnumerable<TEntity>> GetAllAsync();
}
