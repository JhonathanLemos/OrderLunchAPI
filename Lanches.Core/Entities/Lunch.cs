
namespace Lanches.Core.Entities;

public class Lunch : BaseEntity
{
    public Lunch(string name, decimal price, string description)
    {
        Id = Guid.NewGuid();
        Name = name;
        Price = price;
        Description = description;
        Ingredients = new List<LunchIngredient>();
    }

    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public string Description { get; private set; }
    public List<LunchIngredient> Ingredients { get; private set; }
}
