namespace Practical17.Domain.Common.AuditingConracts;

public interface IEntity<TKey> where TKey : struct
{
    TKey Id { get; init; }
}
