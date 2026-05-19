namespace Practical17.Domain.Common.AuditingConracts;

public interface IUpdatable
{
    public DateTimeOffset UpdatedAt { get; set; }
    public Guid UpdateBy { get; set; }
}
