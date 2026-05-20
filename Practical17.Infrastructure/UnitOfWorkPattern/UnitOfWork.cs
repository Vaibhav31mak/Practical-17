using Practical17.Infrastructure.Data.DbContext;

namespace Practical17.Infrastructure.UnitOfWorkPattern;

public class UnitOfWork(StudentDbContext context) : IUnitOfWork
{
    private readonly StudentDbContext _context = context;
    private IBaseRepository<Student, Guid>? _students;

    /// <summary>
    /// Lazily initializes and returns the repository for Student entities. 
    /// This property ensures that the repository is created only when it is first accessed.
    /// </summary>
    public IBaseRepository<Student, Guid> Students 
        => _students ??= new Repository<Student, Guid>(_context);

    public async Task<int> CommitAsync()
        => await _context.SaveChangesAsync();
}