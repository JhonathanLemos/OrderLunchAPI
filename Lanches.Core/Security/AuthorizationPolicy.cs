using Microsoft.AspNetCore.Authorization;
using System.Diagnostics.CodeAnalysis;

namespace Lanches.Core.Security
{
    [ExcludeFromCodeCoverage]
    public static class AuthorizationPolicies
    {
        public static void ConfigurePolicies(AuthorizationOptions options)
        {
            options.AddPolicy("Permissions.Ingredient.Create", policy => policy.RequireClaim("Permission", "Permissions.Ingredient.Create"));
            options.AddPolicy("Permissions.Ingredient.View", policy => policy.RequireClaim("Permission", "Permissions.Ingredient.View"));
            options.AddPolicy("Permissions.Ingredient.Edit", policy => policy.RequireClaim("Permission", "Permissions.Ingredient.Edit"));
            options.AddPolicy("Permissions.Ingredient.Delete", policy => policy.RequireClaim("Permission", "Permissions.Ingredient.Delete"));

            options.AddPolicy("Permissions.Lunch.Create", policy => policy.RequireClaim("Permission", "Permissions.Lunch.Create"));
            options.AddPolicy("Permissions.Lunch.View", policy => policy.RequireClaim("Permission", "Permissions.Lunch.View"));
            options.AddPolicy("Permissions.Lunch.Edit", policy => policy.RequireClaim("Permission", "Permissions.Lunch.Edit"));
            options.AddPolicy("Permissions.Lunch.Delete", policy => policy.RequireClaim("Permission", "Permissions.Lunch.Delete"));

            options.AddPolicy("Permissions.Order.Create", policy => policy.RequireClaim("Permission", "Permissions.Order.Create"));
            options.AddPolicy("Permissions.Order.View", policy => policy.RequireClaim("Permission", "Permissions.Order.View"));
            options.AddPolicy("Permissions.Order.Edit", policy => policy.RequireClaim("Permission", "Permissions.Order.Edit"));
            options.AddPolicy("Permissions.Order.Delete", policy => policy.RequireClaim("Permission", "Permissions.Order.Delete"));
        }
    }
}
