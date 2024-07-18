using Blogger.IntegrationTests.Fixtures;
using Lanches.Application.Dtos;
using Lanches.Application.Queries.GetAllOrder;
using Lanches.Core.Entities;
using Lanches.Infraestructure.Repositories;
using Lanches.Tests.Application.Ingredients;

namespace Lanches.Test.Application.Ingredients.Queries
{
    public class GetAllIngredientsQueryHandlerTests : IClassFixture<LanchesDbContextFixture>
    {
        private readonly LanchesDbContextFixture _context;

        public GetAllIngredientsQueryHandlerTests(LanchesDbContextFixture context)
        {
            _context = context;
        }

        [Fact]
        public async Task GetAllIngredientsHandler_WhenCalled_ReturnsAllIngredients()
        {
            using var context = _context.BuildDbContext("TestDb");
            var ingredientRepository = new GenericRepository<Ingredient>(context);
            await new IngredientFactoryItems(context).CreateItem();

            var ingredientsList = new List<Ingredient>
            {
                new Ingredient("Ingredient 1"),
                new Ingredient("Ingredient 2"),
                new Ingredient("Ingredient 3")
            };


            var ingredientQuery = IngredientFactory.GetAllIngredients(new GetAll("", 1, 10));

            var getAllIngredientQueryHandler = new GetAllIngredientsQueryHandler(ingredientRepository);

            var ingredientViewModel = await getAllIngredientQueryHandler.Handle(ingredientQuery, new CancellationToken());

        }
    }
}
