namespace Practical17.Application.DependencyInjection;

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
