namespace Practical17.Infrastructure.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string MobileNumber { get; set; }
}