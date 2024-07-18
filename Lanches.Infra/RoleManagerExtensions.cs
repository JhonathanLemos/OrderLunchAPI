using Microsoft.AspNetCore.Identity;

namespace Lanches.Infra
{
    public static class RoleManagerExtensions
    {
        public static async Task AddPermissionClaims(this RoleManager<IdentityRole> roleManager, IdentityRole role, string[] permissions)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            foreach (var permission in permissions)
            {
                if (!allClaims.Any(c => c.Type == "Permission" && c.Value == permission))
                {
                    await roleManager.AddClaimAsync(role, new System.Security.Claims.Claim("Permission", permission));
                }
            }
        }
    }
}
