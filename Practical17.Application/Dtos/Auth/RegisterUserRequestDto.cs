namespace Practical17.Application.Dtos.Auth;

// DTO for user registration request. This record encapsulates the necessary information
// required to register a new user.
public sealed record RegisterUserRequestDto(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string MobileNumber,
    string Role);
