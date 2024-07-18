using Lanches.Core.Entities;
using MediatR;

namespace Lanches.Application.Commands.Ingredients.CreateIngredient
{
    public class CreateIngredientCommand : BaseEntity, IRequest<Guid>
    {
        public string Name { get; set; }
    }
}
