namespace Practical17.Application.Dtos.Auth;

// Sealed record for authentication result, containing the JWT token and the list of roles
// associated with the authenticated user.
public sealed record AuthResultDto(string Token, IReadOnlyList<string> Roles);
