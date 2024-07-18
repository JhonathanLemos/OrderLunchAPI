using System.Diagnostics.CodeAnalysis;

namespace Lanches.Core.Security
{
    [ExcludeFromCodeCoverage]
    public static class Permissions
    {
        public static class IngredientPermissions
        {
            public static readonly string[] All = { Create, View, Edit, Delete };
            public const string Create = "Permissions.Ingredient.Create";
            public const string View = "Permissions.Ingredient.View";
            public const string Edit = "Permissions.Ingredient.Edit";
            public const string Delete = "Permissions.Ingredient.Delete";
        }

        public static class LunchPermissions
        {
            public static readonly string[] All = { Create, View, Edit, Delete };
            public const string Create = "Permissions.Lunch.Create";
            public const string View = "Permissions.Lunch.View";
            public const string Edit = "Permissions.Lunch.Edit";
            public const string Delete = "Permissions.Lunch.Delete";
        }

        public static class OrderPermissions
        {
            public static readonly string[] Admin = { View };
            public const string Create = "Permissions.Order.Create";
            public const string View = "Permissions.Order.View";
            public const string Edit = "Permissions.Order.Edit";
            public const string Delete = "Permissions.Order.Delete";
        }
    }
}
