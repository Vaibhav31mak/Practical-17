using Practical17.Application.Dtos.Auth;
using Practical17.Domain.Common.ResultPattern;

namespace Practical17.Application.Contracts;

public interface IAuthService
{
    Task<Result<AuthResultDto>> LoginAsync(LoginRequestDto request);
    Task<Result<(Guid UserId, string Email, string Role)>> RegisterUserAsync(RegisterUserRequestDto request);
}
