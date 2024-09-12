using Domain.Entities;

namespace DAL.Repositories.Interfaces.Generics;
public interface IGenericWriteRepository<TKey, TEntity> where TEntity : Entity
{
    /// <summary>
    /// Create a new entity in the database.
    /// </summary>
    /// <param name="entity">
    /// Entity to create.
    /// </param>
    /// <returns>
    ///  Entity created.
    /// </returns>
    public Task<TEntity> CreateAsync(TEntity entity);

    /// <summary>
    /// Modify an entity in the database.
    /// </summary>
    /// <param name="entity">
    /// Entity with the new values.
    /// </param>
    /// <returns>
    ///    Entity modified.
    /// </returns>
    /// <exception cref="Domain.Exceptions.NotFoundEntityException{TEntity}">
    ///  Entity with the given identifier is not found.
    /// </exception>
    public Task<TEntity> ModifyAsync(TEntity entity);

    /// <summary>
    /// Archive an entity in the database.
    /// </summary>
    /// <param name="id">
    /// Unique identifier of the entity to archive.
    /// </param>
    /// <returns>
    ///  Task completed.
    /// </returns>
    /// <exception cref="Domain.Exceptions.NotFoundEntityException{TEntity}">
    ///  Entity with the given identifier is not found.
    /// </exception>
    public Task DeleteAsync(TKey id);
}
