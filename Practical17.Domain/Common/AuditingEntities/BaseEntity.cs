namespace Practical17.Domain.Common.AuditingEntities;

public class BaseEntity<TId> : ICreatable,
    IUpdatable,
    ISoftDeletable,
    IConcurrencyCheck,
    IEntity<TId> where TId : struct
{
    public TId Id { get; init; } = default!;
    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;
    public Guid CreatedBy { get; init; }
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
    public Guid UpdateBy { get; set; }
    public DateTimeOffset DeletedAt { get; set; }
    public Guid DeletedBy { get; set; }
    public bool IsDeleted { get; set; } = false;
    public byte[] RowVersion { get; set; } = [];
}
