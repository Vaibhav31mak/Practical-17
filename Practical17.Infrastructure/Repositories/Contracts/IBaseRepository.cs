namespace Practical17.Infrastructure.Repositories.Contracts;

// Base repository interface defining common CRUD operations for entities with a specified key type.
// Used Generics for the type of entity and its key, allowing for flexibility and reusability across
// different entity types in the application.
public interface IBaseRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : struct
{
    Task<TEntity?> GetByIdAsync(TKey id);
    Task<IReadOnlyList<TEntity>> GetAllAsync();
    Task AddAsync(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}