namespace Practical17.Application.Dtos.Auth;

public sealed record RegisterUserRequestDto(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string MobileNumber,
    string Role);
