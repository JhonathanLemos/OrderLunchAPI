using Lanches.Application.Dtos.ViewModels;
using Lanches.Core.Entities;
using Lanches.Core.Repositories;
using MediatR;

namespace Lanches.Application.Queries.Ingredients.GetIngredient
{
    public class GetIngredientQueryHandler : IRequestHandler<GetIngredientQuery, IngredientViewModel>
    {
        private readonly IGenericRepository<Ingredient> _repository;

        public GetIngredientQueryHandler(IGenericRepository<Ingredient> repository)
        {
            _repository = repository;
        }

        public async Task<IngredientViewModel> Handle(GetIngredientQuery request, CancellationToken cancellationToken)
        {
            var ingredient = await _repository.GetByIdAsync(request.Id);
            return ingredient == null ? null : IngredientViewModel.FromEntity(ingredient);
        }
    }
}
