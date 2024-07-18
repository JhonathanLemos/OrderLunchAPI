using Lanches.Core.Entities;
using Lanches.Infraestructure.Context;
using Lanches.Infraestructure.Repositories;
using Lanches.Tests.Application.Ingredients;
using Microsoft.EntityFrameworkCore;

namespace Lanches.Tests.Application.Lunchs
{
    public class LunchFactoryItems
    {
        private readonly LanchesDbContext _context;

        public LunchFactoryItems(LanchesDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> CreateItem()
        {
            var lunchRepository = new GenericRepository<Lunch>(_context);
            var ingredientRepository = new GenericRepository<Ingredient>(_context);
            var ingredientId = await new IngredientFactoryItems(_context).CreateItem();
            var ingredient = await ingredientRepository.GetByIdAsync(ingredientId);
            var lunch = new Lunch("Lunch", 10, "Description");
            await lunchRepository.CreateAsync(lunch);
            lunch.Ingredients.Add(new LunchIngredient(lunch, ingredient));
            await _context.SaveChangesAsync();
            return lunch.Id;
        }
    }
}
