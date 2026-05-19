namespace Practical17.Domain.Common.AuditingConracts;

public interface ICreatable
{
    public DateTimeOffset CreatedAt { get; init; }
    public Guid CreatedBy { get; init; }
}
