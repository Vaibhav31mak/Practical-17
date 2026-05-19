namespace Practical17.Infrastructure.Data.Interceptors;

public sealed class AuditingSaveChangesInterceptor : SaveChangesInterceptor
{
    public Guid CurrentUserId { get; set; } = Guid.Empty;

    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        var context = eventData.Context;
        if (context == null) return base.SavingChanges(eventData, result);

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

        return base.SavingChanges(eventData, result);
    }
}
