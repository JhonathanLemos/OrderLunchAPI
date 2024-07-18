using Lanches.Application.Dtos.ViewModels;
using Lanches.Core.Entities;
using Lanches.Core.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Lanches.Application.Queries.Lunchs.GetIngredientsByLunchs
{
    public class GetIngredientsByLunchIdQueryHandler : IRequestHandler<GetIngredientsByLunchIdQuery, List<IngredientViewModel>>
    {
        private readonly IGenericRepository<LunchIngredient> _repository;

        public GetIngredientsByLunchIdQueryHandler(IGenericRepository<LunchIngredient> repository)
        {
            _repository = repository;
        }

        public async Task<List<IngredientViewModel>> Handle(GetIngredientsByLunchIdQuery request, CancellationToken cancellationToken)
        {
            var ingredients = await _repository.GetAll().Where(x => x.LunchId == request.Id).Select(x => x.Ingredient).ToListAsync();
            return ingredients.Select(IngredientViewModel.FromEntity).ToList();
        }
    }
}
