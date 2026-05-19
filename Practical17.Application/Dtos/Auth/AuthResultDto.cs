namespace Practical17.Application.Dtos.Auth;

public sealed record AuthResultDto(string Token, IReadOnlyList<string> Roles);
