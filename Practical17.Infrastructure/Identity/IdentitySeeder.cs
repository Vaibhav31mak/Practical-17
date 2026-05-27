namespace Practical17.Infrastructure.Identity;

public static class IdentitySeeder
{
    /// <summary>
    /// To seed the default admin user and roles into the database. 
    /// Seeds only when no data of admin exists to avoid duplication.
    /// </summary>
    /// <param name="services"></param>
    public static async Task SeedAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
        await EnsureRoleAsync(roleManager, "Admin");
        await EnsureRoleAsync(roleManager, "User");

        var adminEmail = "admin@gmail.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser != null) return;
        adminUser = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            FirstName = "System",
            LastName = "Admin",
            MobileNumber = "9000000000"
        };

        var createResult = await userManager.CreateAsync(adminUser, "Admin@123");
        if (createResult.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }

    /// <summary>
    /// Ensures that a role with the specified name exists. 
    /// </summary>
    /// <param name="roleManager"></param>
    /// <param name="roleName"></param>
    /// <returns>boolean indicating if the role was created</returns>
    private static async Task<bool> EnsureRoleAsync(RoleManager<ApplicationRole> roleManager, string roleName)
    {
        var roleExists = await roleManager.RoleExistsAsync(roleName);
        if (!roleExists)
        {
            await roleManager.CreateAsync(new ApplicationRole { Name = roleName });
        }
        return true;
    }
}
