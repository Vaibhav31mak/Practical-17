using Practical17.Infrastructure.Data.DbContext;

namespace Practical17.Infrastructure.Repositories.Implementations;

// Using Base repository pattern to implement basic CRUD operations for
// entities in the database.
// I have used generic type parameters to make the repository reusable for
// different entity types and their corresponding key types.
public sealed class Repository<TEntity, TKey>(StudentDbContext context) 
    : IBaseRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : struct
{
    private readonly StudentDbContext _context = context;
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    /// <summary>
    /// Retrieves an entity by its unique identifier asynchronously.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Entity if found, otherwise null</returns>
    public async Task<TEntity?> GetByIdAsync(TKey id)
        => await _dbSet.FindAsync(id);

    /// <summary>
    /// Retrieves all entities of the specified type from the database asynchronously.
    /// </summary>
    /// <returns>List of entities</returns>
    public async Task<IReadOnlyList<TEntity>> GetAllAsync()
        // I have used ReadOnlyList to ensure that the returned list cannot be modified,
        // which helps to maintain data integrity and encapsulation.
        => await _dbSet.ToListAsync();

    /// <summary>
    /// Adds a new entity to the database asynchronously.
    /// </summary>
    /// <param name="entity"></param>
    public async Task AddAsync(TEntity entity)
        => await _dbSet.AddAsync(entity);

    /// <summary>
    /// Updates an existing entity in the database. 
    /// </summary>
    /// <param name="entity"></param>
    public void Update(TEntity entity)
        => _dbSet.Update(entity);

    /// <summary>
    /// Deletes an entity from the database.
    /// </summary>
    /// <param name="entity"></param>
    public void Delete(TEntity entity)
        => _dbSet.Remove(entity);
}