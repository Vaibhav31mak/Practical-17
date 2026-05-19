namespace Practical17.Domain.Entities;

public class Student : BaseEntity<Guid>
{
    public required string StudentName { get; set; }
    public required string RollNumber { get; set; }
    public required string Course { get; set; }
    public required DateTimeOffset DateOfBirth { get; set; }
    //public virtual ApplicationUser? AssignedUser { get; set; }
}
