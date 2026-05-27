namespace Practical17.Application.DependencyInjection;

// Added this extension method on IServiceCollection to register all the services
// related to application layer in one place to keep program.cs file clean and maintainable.
public static class ApplicationServicesRegistration
{
    public static IServiceCollection AddApplicationServices
        (this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => cfg.AddProfile<StudentProfile>());
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<IAuthService, AuthService>();
        return services;
    }
}
