using Practical17.Application.Contracts;
using Practical17.Application.Dtos.Auth;
using Practical17.Domain.Common.ResultPattern;
using Practical17.Infrastructure.Identity;

namespace Practical17.Application.Services;

// Sealed class for authentication related operations, such as login and registration.
// This class uses ASP.NET Core Identity for user management and JWT for token generation.
public sealed class AuthService(
    UserManager<ApplicationUser> userManager,
    IConfiguration configuration,
    ILogger<AuthService> logger) : IAuthService
{
    /// <summary>
    /// Login method that validates user credentials and generates a JWT token if successful. 
    /// Generates a JWT token containing user claims and roles, which can be used for authentication 
    /// in subsequent requests.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Register method that creates a new user with the specified details and assigns them a role.
    /// </summary>
    /// <param name="request"></param>
    /// <returns>Returns the created user's credentials if successful, otherwise returns a failure result.</returns>
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

    /// <summary>
    /// Creates a JWT token for the authenticated user, including their claims and roles.
    /// </summary>
    /// <param name="user"></param>
    /// <param name="roles"></param>
    /// <param name="configuration"></param>
    /// <returns>The generated JWT token.</returns>
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
