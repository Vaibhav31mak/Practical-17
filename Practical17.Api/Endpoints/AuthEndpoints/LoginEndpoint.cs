namespace Practical17.Api.Endpoints.AuthEndpoints;

public sealed class LoginEndpoint(IAuthService authService) : BaseEndpoint
{
    [HttpPost("api/auth/login")]
    [AllowAnonymous]
    public async Task<IActionResult> HandleAsync([FromBody] LoginRequestDto request)
    {
        var result = await authService.LoginAsync(request);
        if (!result.IsSuccess)
            return Unauthorized(result.ErrorMessage);

        return Ok(result.Value);
    }
}
