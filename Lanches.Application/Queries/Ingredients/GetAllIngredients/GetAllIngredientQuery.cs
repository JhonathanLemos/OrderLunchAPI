using Lanches.Application.Dtos;
using Lanches.Application.Dtos.ViewModels;
using Lanches.Core.Models;
using MediatR;

namespace Lanches.Application.Queries.GetIngredient
{
    public class GetAllIngredientQuery : IRequest<PaginationResult<IngredientViewModel>>
    {
        public GetAll Query { get; set; }

        public GetAllIngredientQuery(GetAll query)
        {
            Query = query;
        }
    }
}
