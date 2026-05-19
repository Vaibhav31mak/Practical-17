namespace Practical17.Infrastructure.Data.Configurations;

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

        /*
        builder.HasOne(s => s.AssignedUser)
            .WithMany() // Or .WithMany(u => u.Students) if User has a collection
            .HasForeignKey("AssignedUserId") // Shadow property or actual property
            .OnDelete(DeleteBehavior.SetNull); // or Restrict/Cascade
        */
    }
}