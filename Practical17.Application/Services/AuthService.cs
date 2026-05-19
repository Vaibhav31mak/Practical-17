using Practical17.Application.Contracts;
using Practical17.Application.Dtos.Auth;
using Practical17.Domain.Common.ResultPattern;
using Practical17.Infrastructure.Identity;

namespace Practical17.Application.Services;

public sealed class AuthService(
    UserManager<ApplicationUser> userManager,
    IConfiguration configuration,
    ILogger<AuthService> logger) : IAuthService
{
    public async Task<Result<AuthResultDto>> LoginAsync(LoginRequestDto request)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null || !await userManager.CheckPasswordAsync(user, request.Password))
            return Result<AuthResultDto>.Failure("Invalid credentials.");

        var roles = await userManager.GetRolesAsync(user);
        var token = CreateToken(user, roles, configuration);

        logger.LogInformation("User {UserId} logged in", user.Id);
        return Result<AuthResultDto>.Success(new AuthResultDto(token, roles.ToList()));
    }

    public async Task<Result<(Guid UserId, string Email, string Role)>> RegisterUserAsync(RegisterUserRequestDto request)
    {
        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            MobileNumber = request.MobileNumber
        };

        var result = await userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(error => error.Description));
            return Result<(Guid, string, string)>.Failure(errors);
        }

        var roleResult = await userManager.AddToRoleAsync(user, request.Role);
        if (!roleResult.Succeeded)
        {
            var errors = string.Join(", ", roleResult.Errors.Select(error => error.Description));
            return Result<(Guid, string, string)>.Failure(errors);
        }

        logger.LogInformation("Registered user {UserId} with role {Role}", user.Id, request.Role);
        return Result<(Guid, string, string)>.Success((user.Id, user.Email ?? string.Empty, request.Role));
    }

    private static string CreateToken(ApplicationUser user, IList<string> roles, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("Jwt");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email ?? string.Empty),
            new(ClaimTypes.Name, user.UserName ?? string.Empty)
        };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
