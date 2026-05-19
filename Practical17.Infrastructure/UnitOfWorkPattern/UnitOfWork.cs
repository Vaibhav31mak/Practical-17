namespace Practical17.Infrastructure.UnitOfWorkPattern;

public class UnitOfWork(StudentDbContext context) : IUnitOfWork
{
    private readonly StudentDbContext _context = context;
    private IBaseRepository<Student, Guid>? _students;

    public IBaseRepository<Student, Guid> Students 
        => _students ??= new Repository<Student, Guid>(_context);

    public async Task<int> CommitAsync()
        => await _context.SaveChangesAsync();
}