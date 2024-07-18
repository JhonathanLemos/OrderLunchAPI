using Lanches.Core.Entities;
using Lanches.Core.Security;
using Lanches.Infra;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace YourNamespace
{
    public class RoleInitializer : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public RoleInitializer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

                await CreateRoleWithPermissionsAsync(roleManager, "Admin", Permissions.IngredientPermissions.All.Concat(Permissions.LunchPermissions.All).Concat(Permissions.OrderPermissions.Admin).ToArray());

                await CreateRoleWithPermissionsAsync(roleManager, "Customer", new[]
                {
                    Permissions.LunchPermissions.View,
                    Permissions.OrderPermissions.Create,
                    Permissions.OrderPermissions.View,
                    Permissions.OrderPermissions.Edit,
                    Permissions.OrderPermissions.Delete
                });

                var user = await userManager.FindByEmailAsync("admin@admin.com");
                if (user == null)
                {
                    user = new User { UserName = "Admin", Email = "admin@admin.com" };
                    await userManager.CreateAsync(user, Environment.GetEnvironmentVariable("ADMINPASSWORD"));

                    await userManager.AddToRoleAsync(user, "Admin");

                    var adminRole = await roleManager.FindByNameAsync("Admin");
                    var adminPermissions = await roleManager.GetClaimsAsync(adminRole);
                    foreach (var permission in adminPermissions)
                    {
                        await userManager.AddClaimAsync(user, permission);
                    }
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        private async Task CreateRoleWithPermissionsAsync(RoleManager<IdentityRole> roleManager, string roleName, string[] permissions)
        {
            IdentityRole role;
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                role = new IdentityRole(roleName);
                await roleManager.CreateAsync(role);
            }
            else
            {
                role = await roleManager.FindByNameAsync(roleName);
            }

            await roleManager.AddPermissionClaims(role, permissions);
        }
    }
}
