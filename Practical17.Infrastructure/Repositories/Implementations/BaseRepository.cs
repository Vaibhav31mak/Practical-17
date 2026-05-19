namespace Practical17.Infrastructure.Repositories.Implementations;
public sealed class Repository<TEntity, TKey>(StudentDbContext context) 
    : IBaseRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : struct
{
    private readonly StudentDbContext _context = context;
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public async Task<TEntity?> GetByIdAsync(TKey id)
        => await _dbSet.FindAsync(id);
    
    public async Task<IReadOnlyList<TEntity>> GetAllAsync()
        => await _dbSet.ToListAsync();

    public async Task AddAsync(TEntity entity)
        => await _dbSet.AddAsync(entity);

    public void Update(TEntity entity)
        => _dbSet.Update(entity);

    public void Delete(TEntity entity)
        => _dbSet.Remove(entity);
}