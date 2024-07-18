using Blogger.IntegrationTests.Fixtures;
using Lanches.Application.Commands.Lunchs.CreateLunch;
using Lanches.Application.Services;
using Lanches.Core.Entities;
using Lanches.Infraestructure.Context;
using Lanches.Infraestructure.Repositories;
using Lanches.Tests.Application.Ingredients;

namespace Lanches.Tests.Application.Lunchs.Commands
{
    public class CreateLunchCommandHandlerTests : IClassFixture<LanchesDbContextFixture>
    {
        private readonly LanchesDbContextFixture _context;

        public CreateLunchCommandHandlerTests(LanchesDbContextFixture context)
        {
            _context = context;
        }

        [Fact]
        public async Task CreateLunchHandler_WhenCalled_ReturnsValidId()
        {
            using var context = _context.BuildDbContext("TestDbs");

            var lunchService = CreateLunchServiceWithDependencies(context);

            var ingredientId = await new IngredientFactoryItems(context).CreateItem();

            var newLunch = LunchFactory.GetLunchToCreate();
            newLunch.Ingredients.Add(ingredientId);
            var createLunchCommandHandler = new CreateLunchCommandHandler(lunchService);
            var id = await createLunchCommandHandler.Handle(newLunch, new CancellationToken());

            Assert.True(id >= Guid.Empty);
        }

        private ICreateLunchService CreateLunchServiceWithDependencies(LanchesDbContext context)
        {
            var lunchRepository = new GenericRepository<Lunch>(context);
            var ingredientRepository = new GenericRepository<Ingredient>(context);
            var lunchIngredientRepository = new GenericRepository<LunchIngredient>(context);

            return new CreateLunchService(lunchRepository, lunchIngredientRepository, ingredientRepository);
        }
    }
}
