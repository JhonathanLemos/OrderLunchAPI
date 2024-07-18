using Lanches.Application.Dtos.ViewModels;
using MediatR;

namespace Lanches.Application.Queries.Lunchs.GetIngredientsByLunchs
{
    public class GetIngredientsByLunchIdQuery : IRequest<List<IngredientViewModel>>
    {
        public GetIngredientsByLunchIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
