using MediatR;

namespace Lanches.Application.Commands.Ingredients.UpdateIngredient
{
    public class UpdateIngredientCommand : IRequest<Guid?>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public void SetId(Guid id)
        {
            Id = id;
        }

        public UpdateIngredientCommand(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public UpdateIngredientCommand()
        {

        }
    }
}
