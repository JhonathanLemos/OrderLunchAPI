namespace Lanches.Core.Entities
{
    public class LunchIngredient : BaseEntity
    {
        public LunchIngredient()
        {

        }
        public LunchIngredient(Lunch lunch, Ingredient ingredient)
        {
            Lunch = lunch;
            LunchId = lunch.Id;
            IngredientId = ingredient.Id;
            Ingredient = ingredient;
        }
        public Guid LunchId { get; private set; }
        public Lunch Lunch { get; private set; }

        public Guid IngredientId { get; private set; }
        public Ingredient Ingredient { get; private set; }

        public int Quantity { get; set; }
    }

}
