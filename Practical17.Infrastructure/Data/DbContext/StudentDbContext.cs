using System.Reflection;

namespace Practical17.Infrastructure.Data.DbContext;

// This class represents the Identity DbContext for the application,
// which includes both the Identity tables and the application-specific tables.
public class StudentDbContext(DbContextOptions<StudentDbContext> options)
    : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>(options)
{
    public DbSet<Student> Students => Set<Student>();
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}