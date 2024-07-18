using Blogger.IntegrationTests.Fixtures;
using Lanches.Application.Queries.Lunchs.GetIngredientsByLunchs;
using Lanches.Core.Entities;
using Lanches.Infraestructure.Repositories;
using Lanches.Tests.Application.Ingredients;

namespace Lanches.Tests.Application.Lunchs.Queries
{
    public class GetIngredientByLunchIdQueryHandlerTests : IClassFixture<LanchesDbContextFixture>
    {
        private readonly LanchesDbContextFixture _context;

        public GetIngredientByLunchIdQueryHandlerTests(LanchesDbContextFixture context)
        {
            _context = context;
        }

        [Fact]
        public async Task GetIngredientByLunchIdHandler_WhenCalled_ReturnsValidIngredient()
        {
            using var context = _context.BuildDbContext("TestDb");
            var lunchIngredientRepository = new GenericRepository<LunchIngredient>(context);
            var newIngredient = IngredientFactory.GetIngredientsByLunchIdQuery();
            var getIngredientQueryHandler = new GetIngredientsByLunchIdQueryHandler(lunchIngredientRepository);
            await new LunchFactoryItems(context).CreateItem();
            var ingredientViewModel = await getIngredientQueryHandler.Handle(newIngredient, new CancellationToken());

            Assert.True(ingredientViewModel.Count >= 0);
            Assert.NotNull(ingredientViewModel);
        }
    }
}
