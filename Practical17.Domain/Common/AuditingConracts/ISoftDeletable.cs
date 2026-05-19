namespace Practical17.Domain.Common.AuditingConracts;

public interface ISoftDeletable
{
    public DateTimeOffset DeletedAt { get; set; }
    public Guid DeletedBy { get; set; }
    public bool IsDeleted { get; set; }
}
