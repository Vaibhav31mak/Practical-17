namespace Practical17.Infrastructure.Identity;

// User for authentication and authorization, extending IdentityUser
// with additional properties for first name, last name, and mobile number.
public class ApplicationUser : IdentityUser<Guid>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string MobileNumber { get; set; }
}