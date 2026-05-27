namespace Practical17.Domain.Common.AuditingConracts;

// Creatable interface defines the properties required for tracking the creation of an entity.
public interface ICreatable
{
    public DateTimeOffset CreatedAt { get; init; }
    public Guid CreatedBy { get; init; }
}
