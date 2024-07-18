using AutoMapper;
using Lanches.Application.Commands.Lunchs.UpdateLunch;
using Lanches.Core.Entities;
using Lanches.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Lanches.Application.Services
{
    public class UpdateLunchService : IUpdateLunchService
    {
        private readonly IGenericRepository<Lunch> _repository;
        private readonly IGenericRepository<LunchIngredient> _lunchIngredientRepository;
        private readonly IGenericRepository<Ingredient> _ingredientRepository;
        private readonly IMapper _mapper;

        public UpdateLunchService(IGenericRepository<Lunch> repository, IGenericRepository<LunchIngredient> lunchIngredientRepository, IGenericRepository<Ingredient> ingredientRepository, IMapper mapper)
        {
            _repository = repository;
            _lunchIngredientRepository = lunchIngredientRepository;
            _ingredientRepository = ingredientRepository;
            _mapper = mapper;
        }

        public async Task<Guid> UpdateLunchAsync(UpdateLunchCommand request, CancellationToken cancellationToken)
        {
            var ingredients = _lunchIngredientRepository.GetAll().Where(x => x.LunchId == request.Id).ToList();
            await _lunchIngredientRepository.DeleteRange(ingredients);

            var existingLunch = await _repository.GetByIdAsync(request.Id);

            if (existingLunch is null)
                throw new Exception($"Lunch with ID {request.Id} not found.");

            _mapper.Map(request, existingLunch);
            var id = await _repository.Update(existingLunch);

            foreach (var ingredientId in request.Ingredients)
            {
                var ingredient = await _ingredientRepository.GetByIdAsync(ingredientId);
                if (ingredient is null)
                    throw new Exception($"Ingredient with ID {ingredientId} not found.");

                var newLunchIngredient = new LunchIngredient(existingLunch, ingredient);
                await _lunchIngredientRepository.CreateAsync(newLunchIngredient);
            }
            return id;
        }
    }
}
