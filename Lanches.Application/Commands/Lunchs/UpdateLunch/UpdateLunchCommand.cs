using MediatR;

namespace Lanches.Application.Commands.Lunchs.UpdateLunch
{
    public class UpdateLunchCommand : IRequest<Guid>
    {
        public UpdateLunchCommand(Guid id, string name, decimal price, string description, List<Guid> ingredients)
        {
            Id = id;
            Name = name;
            Price = price;
            Description = description;
            Ingredients = ingredients;
        }
        public Guid Id { get; private set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public List<Guid> Ingredients { get; set; }

        public void SetId(Guid id)
        {
            Id = id;
        }
    }
}
