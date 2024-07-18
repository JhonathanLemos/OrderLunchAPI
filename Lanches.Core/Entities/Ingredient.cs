
using System.Text.Json.Serialization;

namespace Lanches.Core.Entities
{
    public class Ingredient : BaseEntity
    {
        public Ingredient(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public string Name { get; private set; }

        [JsonIgnore]
        public List<LunchIngredient> Lunchs { get; private set; }
    }
}
