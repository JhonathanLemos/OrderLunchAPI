using Blogger.IntegrationTests.Fixtures;
using Lanches.Application.Commands.Ingredients.CreateIngredient;
using Lanches.Core.Entities;
using Lanches.Infraestructure.Repositories;

namespace Lanches.Tests.Application.Ingredients.Commands
{
    public class CreateIngredientCommandHandlerTests : IClassFixture<LanchesDbContextFixture>
    {
        private readonly LanchesDbContextFixture _context;

        public CreateIngredientCommandHandlerTests(LanchesDbContextFixture context)
        {
            _context = context;
        }

        [Fact]
        public async Task CreateIngredientHandler_WhenCalled_ReturnsValidId()
        {
            using var context = _context.BuildDbContext("TestDb");
            var ingredientRepository = new GenericRepository<Ingredient>(context);
            var newIngredient = IngredientFactory.GetIngredientToCreate();
            var createProjectCommandHandler = new CreateIngredientCommandHandler(ingredientRepository);
            var id = await createProjectCommandHandler.Handle(newIngredient, new CancellationToken());
            Assert.True(id >= Guid.Empty);
        }
    }
}
