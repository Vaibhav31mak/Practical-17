namespace Practical17.Api.Endpoints.AuthEndpoints;

// Endpoint for user login
public sealed class LoginEndpoint(IAuthService authService) : BaseEndpoint
{
    [HttpPost("api/auth/login")]
    [AllowAnonymous]
    /// <summary>
    /// Handles the login request.
    /// </summary>
    /// <param name="request">The login request data.</param>
    /// <returns>An IActionResult indicating the result of the login attempt.</returns>
    public async Task<IActionResult> HandleAsync([FromBody] LoginRequestDto request)
    {
        var result = await authService.LoginAsync(request);
        if (!result.IsSuccess)
            return Unauthorized(result.ErrorMessage);

        return Ok(result.Value);
    }
}
