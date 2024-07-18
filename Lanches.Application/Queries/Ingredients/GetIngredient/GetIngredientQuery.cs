using Lanches.Application.Dtos.ViewModels;
using MediatR;

namespace Lanches.Application.Queries.Ingredients.GetIngredient
{
    public class GetIngredientQuery : IRequest<IngredientViewModel>
    {
        public GetIngredientQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
