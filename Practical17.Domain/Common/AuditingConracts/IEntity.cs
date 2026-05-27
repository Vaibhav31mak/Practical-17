namespace Practical17.Domain.Common.AuditingConracts;

// IEntity interface defines the basic properties that an entity must have.
// Here we are using a generic type parameter TKey to allow for different
// types of primary keys (e.g., int, Guid, long, so on).
public interface IEntity<TKey> where TKey : struct
{
    TKey Id { get; init; }
}
