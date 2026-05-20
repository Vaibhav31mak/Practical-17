namespace Practical17.Infrastructure.Data.Configurations;

// Writing Data Annotations on the entity classes violates SRP.
// Morever writing the validations in the DbContext class using
// Fluent API makes it bulky and hard to maintain.
// So, I am using the IEntityTypeConfiguration<T> interface to
// separate the configuration of the Student entity into its own class.
public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("Students");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.StudentName)
            .IsRequired()
            .HasMaxLength(100); 

        builder.Property(s => s.RollNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(s => s.Course)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.DateOfBirth)
            .IsRequired();

        builder.HasIndex(s => s.RollNumber)
            .IsUnique();
    }
}