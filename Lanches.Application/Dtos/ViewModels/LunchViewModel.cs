using Lanches.Core.Entities;

namespace Lanches.Application.Dtos.ViewModels
{
    public class LunchViewModel : BaseEntity
    {
        public LunchViewModel(Guid id, string name, decimal price, string description)
        {
            Id = id;
            Name = name;
            Price = price;
            Description = description;
        }

        public static LunchViewModel FromEntity(Lunch lunch)
        {
            return new LunchViewModel(lunch.Id, lunch.Name, lunch.Price, lunch.Description);
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public string Description { get; private set; }
    }
}
