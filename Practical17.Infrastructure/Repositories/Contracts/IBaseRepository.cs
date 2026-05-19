namespace Practical17.Infrastructure.Repositories.Contracts;

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