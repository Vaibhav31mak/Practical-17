namespace Practical17.Infrastructure.Data.Filters;

/// <summary>
/// Global Filter for Soft Delete functionality. 
/// This extension method is used to apply a global query filter to all 
/// entities that implement the ISoftDeletable interface.
/// </summary>
public static class ModelBuilderExtensions
{
    public static ModelBuilder ApplySoftDeleteQueryFilters(this ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(ISoftDeletable).IsAssignableFrom(entityType.ClrType))
            {
                var parameter = Expression.Parameter(entityType.ClrType, "e");
                var body = Expression.Equal(
                    Expression.Property(parameter, nameof(ISoftDeletable.IsDeleted)),
                    Expression.Constant(false));

                var filter = Expression.Lambda(body, parameter);
                entityType.SetQueryFilter(filter);
            }
        }

        return modelBuilder;
    }
}
