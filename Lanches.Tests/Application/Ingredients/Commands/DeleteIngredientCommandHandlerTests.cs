using Blogger.IntegrationTests.Fixtures;
using Lanches.Application.Commands.Ingredients.DeleteIngredient;
using Lanches.Core.Entities;
using Lanches.Infraestructure.Repositories;

namespace Lanches.Tests.Application.Ingredients.Commands
{
    public class DeleteIngredientCommandHandlerTests : IClassFixture<LanchesDbContextFixture>
    {
        private readonly LanchesDbContextFixture _context;

        public DeleteIngredientCommandHandlerTests(LanchesDbContextFixture context)
        {
            _context = context;
        }

        [Fact]
        public async Task DeleteIngredientHandler_WhenCalled_ReturnsValidId()
        {
            using var context = _context.BuildDbContext("TestDb");
            var ingredientRepository = new GenericRepository<Ingredient>(context);
            var ingredientId = await new IngredientFactoryItems(context).CreateItem();
            var newIngredient = IngredientFactory.GetIngredientToDelete(ingredientId);
            var deleteProjectCommandHandler = new DeleteIngredientCommandHandler(ingredientRepository);
            var id = await deleteProjectCommandHandler.Handle(newIngredient, new CancellationToken());
            Assert.True(id >= Guid.Empty);
        }
    }
}
