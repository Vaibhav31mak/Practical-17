namespace Practical17.Infrastructure.Identity;

public static class IdentitySeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

        await EnsureRoleAsync(roleManager, "Admin");
        await EnsureRoleAsync(roleManager, "User");

        var adminEmail = "admin@practical17.local";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser != null) return;

        adminUser = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            FirstName = "System",
            LastName = "Admin",
            MobileNumber = "0000000000"
        };

        var createResult = await userManager.CreateAsync(adminUser, "Admin@12345");
        if (createResult.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }

    private static async Task<bool> EnsureRoleAsync(RoleManager<IdentityRole<Guid>> roleManager, string roleName)
    {
        var roleExists = await roleManager.RoleExistsAsync(roleName);
        if (!roleExists)
        {
            await roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
        }
        return true;
    }
}
