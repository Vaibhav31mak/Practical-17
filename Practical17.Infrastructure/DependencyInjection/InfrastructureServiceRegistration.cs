using Practical17.Infrastructure.Data.DbContext;

namespace Practical17.Infrastructure.DependencyInjection;

// Added this extentsion method on IServiceCollection to register all the services related to infrastructure layer in one place.
// This will be called in the Program.cs file of the API project to add these services to the DI container.
// Also this make program.cs file clean and maintainable by keeping the infrastructure related service registrations in the
// infrastructure layer itself.
public static class InfrastructureServiceRegistration
{
    /// <summary>
    /// This method registers following services for the infrastructure layer:-
    /// => DbContext
    /// => Identity services
    /// => Authentication
    /// => Unit of Work
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<AuditingSaveChangesInterceptor>();

        services.AddDbContext<StudentDbContext>((sp, options) =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            options.AddInterceptors(
            [
                sp.GetRequiredService<AuditingSaveChangesInterceptor>()
            ]);
        });

        services.AddIdentityCore<ApplicationUser>()
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<StudentDbContext>();

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var jwtSettings = configuration.GetSection("Jwt");
                var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
