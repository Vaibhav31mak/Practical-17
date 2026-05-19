namespace Practical17.Domain.Common.AuditingConracts;

public interface IConcurrencyCheck
{
    public byte[] RowVersion { get; set; }
}
