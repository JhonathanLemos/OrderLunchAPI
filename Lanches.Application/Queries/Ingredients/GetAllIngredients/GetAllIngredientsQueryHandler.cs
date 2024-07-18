using Lanches.API.Extensions;
using Lanches.Application.Dtos.ViewModels;
using Lanches.Application.Queries.GetIngredient;
using Lanches.Core.Entities;
using Lanches.Core.Models;
using Lanches.Core.Repositories;
using MediatR;

namespace Lanches.Application.Queries.GetAllOrder
{
    public class GetAllIngredientsQueryHandler : IRequestHandler<GetAllIngredientQuery, PaginationResult<IngredientViewModel>>
    {
        private readonly IGenericRepository<Ingredient> _repository;

        public GetAllIngredientsQueryHandler(IGenericRepository<Ingredient> repository)
        {
            _repository = repository;
        }

        public async Task<PaginationResult<IngredientViewModel>> Handle(GetAllIngredientQuery request, CancellationToken cancellationToken)
        {
            var search = request.Query.Search;
            var ingredients = await _repository.GetAll()
                .WhereIf(string.IsNullOrEmpty(search), x => x.Name.Contains(search))
                .GetPaged(request.Query.Page, request.Query.PageSize);

            return ingredients.ReplaceData(ingredients.Data.Select(IngredientViewModel.FromEntity).ToList());
        }
    }
}
