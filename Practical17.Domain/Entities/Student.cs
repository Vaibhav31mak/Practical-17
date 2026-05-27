namespace Practical17.Domain.Entities;

// This is the Student entity class which represents the Student table in the database.
// The validations are implemented in the infrastructure layer using IEntityTypeConfiguration
// and not using data annotations to keep the entity class clean and maintainable and follow SRP.
public class Student : BaseEntity<Guid>
{
    public required string StudentName { get; set; }
    public required string RollNumber { get; set; }
    public required string Course { get; set; }
    public required DateTimeOffset DateOfBirth { get; set; }
}
