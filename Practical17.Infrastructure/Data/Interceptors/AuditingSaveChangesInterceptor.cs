namespace Practical17.Infrastructure.Data.Interceptors;

public sealed class AuditingSaveChangesInterceptor : SaveChangesInterceptor
{
    public Guid CurrentUserId { get; set; } = Guid.Empty;

    /// <summary>
    /// Overriden SaveChanges method to automatically set auditing properties for 
    /// entities that implement ICreatable, IUpdatable, and ISoftDeletable interfaces.
    /// This is the best practice used for auditing rather than triggers make maintainability,
    /// scalability and circular dependency issues. It also allows us to have more control 
    /// over the auditing process and easily extend it in the future if needed.
    /// </summary>
    /// <param name="eventData"></param>
    /// <param name="result"></param>
    /// <returns>InterceptionResult</returns>
    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        ApplyAuditing(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        ApplyAuditing(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void ApplyAuditing(Microsoft.EntityFrameworkCore.DbContext? context)
    {
        if (context == null)
        {
            return;
        }

        var now = DateTimeOffset.UtcNow;

        foreach (var entry in context.ChangeTracker.Entries())
        {
            if (entry.State == EntityState.Added && entry.Entity is ICreatable)
            {
                entry.Property(nameof(ICreatable.CreatedAt)).CurrentValue = now;
                entry.Property(nameof(ICreatable.CreatedBy)).CurrentValue = CurrentUserId;
            }

            if (entry.State == EntityState.Modified && entry.Entity is IUpdatable)
            {
                entry.Property(nameof(IUpdatable.UpdatedAt)).CurrentValue = now;
                entry.Property(nameof(IUpdatable.UpdateBy)).CurrentValue = CurrentUserId;
            }

            if (entry.State == EntityState.Deleted && entry.Entity is ISoftDeletable)
            {
                entry.State = EntityState.Modified;
                entry.Property(nameof(ISoftDeletable.IsDeleted)).CurrentValue = true;
                entry.Property(nameof(ISoftDeletable.DeletedAt)).CurrentValue = now;
                entry.Property(nameof(ISoftDeletable.DeletedBy)).CurrentValue = CurrentUserId;
            }
        }
    }
}
