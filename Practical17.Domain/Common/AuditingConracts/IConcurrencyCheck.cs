namespace Practical17.Domain.Common.AuditingConracts;

// This interface is used to implement optimistic concurrency control in the entities that implement it.
public interface IConcurrencyCheck
{
    public byte[] RowVersion { get; set; }
}
