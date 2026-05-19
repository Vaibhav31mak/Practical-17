namespace Practical17.Api.Endpoints.AuthEndpoints;

public sealed class RegisterUserEndpoint(IAuthService authService) : BaseEndpoint
{
    [HttpPost("api/auth/register")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> HandleAsync([FromBody] RegisterUserRequestDto request)
    {
        var result = await authService.RegisterUserAsync(request);
        if (!result.IsSuccess)
            return BadRequest(result.ErrorMessage);

        return Ok(new { result.Value.UserId, result.Value.Email, result.Value.Role });
    }
}
