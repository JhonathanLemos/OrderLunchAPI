using Lanches.Core.Entities;

namespace Lanches.Application.Dtos.ViewModels
{
    public class IngredientViewModel : BaseEntity
    {
        public IngredientViewModel(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public static IngredientViewModel FromEntity(Ingredient ingredient) =>
            new IngredientViewModel(ingredient.Id, ingredient.Name);


        public string Name { get; private set; }
    }
}
