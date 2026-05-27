namespace Practical17.Application.Dtos.Auth;

// Sealed record for Login Request Data Transfer Object.
public sealed record LoginRequestDto(string Email, string Password);
