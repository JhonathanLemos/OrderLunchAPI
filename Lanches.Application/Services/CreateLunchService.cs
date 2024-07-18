using Lanches.Application.Commands.Lunchs.CreateLunch;
using Lanches.Core.Entities;
using Lanches.Core.Repositories;

namespace Lanches.Application.Services
{
    public class CreateLunchService : ICreateLunchService
    {
        private readonly IGenericRepository<Lunch> _lunchRepository;
        private readonly IGenericRepository<LunchIngredient> _lunchIngredientRepository;
        private readonly IGenericRepository<Ingredient> _ingredientRepository;

        public CreateLunchService(
            IGenericRepository<Lunch> lunchRepository,
            IGenericRepository<LunchIngredient> lunchIngredientRepository,
            IGenericRepository<Ingredient> ingredientRepository)
        {
            _lunchRepository = lunchRepository;
            _lunchIngredientRepository = lunchIngredientRepository;
            _ingredientRepository = ingredientRepository;
        }

        public async Task<Guid> CreateLunchAsync(CreateLunchCommand request, CancellationToken cancellationToken)
        {
            var newLunch = new Lunch(request.Name, request.Price, request.Description);
            var lunch = await _lunchRepository.CreateAsync(newLunch);

            foreach (var ingredientId in request.Ingredients)
            {
                var ingredient = await _ingredientRepository.GetByIdAsync(ingredientId);
                if (ingredient != null)
                    await _lunchIngredientRepository.CreateAsync(new LunchIngredient(newLunch, ingredient));
                else
                    throw new Exception($"Ingredient with ID {ingredientId} not found.");
            }

            return lunch;
        }
    }
}
