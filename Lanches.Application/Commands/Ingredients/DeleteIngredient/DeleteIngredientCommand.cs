using MediatR;

namespace Lanches.Application.Commands.Ingredients.DeleteIngredient
{
    public class DeleteIngredientCommand : IRequest<Guid?>
    {
        public DeleteIngredientCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
