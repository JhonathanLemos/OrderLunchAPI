using Blogger.IntegrationTests.Fixtures;
using Lanches.Application.Queries.Ingredients.GetIngredient;
using Lanches.Core.Entities;
using Lanches.Infraestructure.Repositories;
using Lanches.Tests.Application.Ingredients;
using Microsoft.EntityFrameworkCore;

namespace Lanches.Test.Application.Ingredients.Queries
{
    public class GetIngredientByIdQueryHandlerTests : IClassFixture<LanchesDbContextFixture>
    {
        private readonly LanchesDbContextFixture _context;

        public GetIngredientByIdQueryHandlerTests(LanchesDbContextFixture context)
        {
            _context = context;
        }

        [Fact]
        public async Task GetIngredientByIdHandler_WhenCalled_ReturnsValidIngredient()
        {
            using var context = _context.BuildDbContext("TestDb");
            var ingredientRepository = new GenericRepository<Ingredient>(context);
            await new IngredientFactoryItems(context).CreateItem();

            var existingIngredient = await context.Ingredients.FirstOrDefaultAsync();
            var getIngredientQueryHandler = new GetIngredientQueryHandler(ingredientRepository);

            var ingredientViewModel = await getIngredientQueryHandler.Handle(new GetIngredientQuery(existingIngredient.Id), CancellationToken.None);

            Assert.NotNull(ingredientViewModel);
            Assert.True(ingredientViewModel.Id >= Guid.Empty);
        }
    }
}
