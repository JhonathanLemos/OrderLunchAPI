using Lanches.Core.Entities;
using Lanches.Infraestructure.Context;
using Lanches.Infraestructure.Repositories;

namespace Lanches.Tests.Application.Ingredients
{
    public class IngredientFactoryItems
    {
        private readonly LanchesDbContext _context;

        public IngredientFactoryItems(LanchesDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> CreateItem()
        {
            var ingredientRepository = new GenericRepository<Ingredient>(_context);
            var ingredientId = await ingredientRepository.CreateAsync(new Ingredient("nameTest"));
            await _context.SaveChangesAsync();
            return ingredientId;
        }
    }
}
