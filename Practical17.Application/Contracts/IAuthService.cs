namespace Practical17.Application.Contracts;

// IAuth service interface defines the contract for authentication-related operations,
// such as user login and registration.
public interface IAuthService
{
    Task<Result<AuthResultDto>> LoginAsync(LoginRequestDto request);
    Task<Result<(Guid UserId, string Email, string Role)>> RegisterUserAsync(RegisterUserRequestDto request);
}
