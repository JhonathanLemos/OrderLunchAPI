using AutoMapper;
using Blogger.IntegrationTests.Fixtures;
using Lanches.Application.Commands.Ingredients.UpdateIngredient;
using Lanches.Core.Entities;
using Lanches.Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Lanches.Tests.Application.Ingredients.Commands
{
    public class UpdateIngredientCommandHandlerTests : IClassFixture<LanchesDbContextFixture>
    {
        private readonly LanchesDbContextFixture _context;

        public UpdateIngredientCommandHandlerTests(LanchesDbContextFixture context)
        {
            _context = context;
        }

        [Fact]
        public async Task UpdateIngredientHandler_WhenCalled_ReturnsValidId()
        {
            using var context = _context.BuildDbContext("TestDb");
            var ingredientRepository = new GenericRepository<Ingredient>(context);
            var mapper = new Mock<IMapper>();
            var ingredientFactoryItems = new IngredientFactoryItems(context);
            await ingredientFactoryItems.CreateItem();

            var existingIngredient = await context.Ingredients.AsNoTracking().FirstOrDefaultAsync(i => i.Name == "nameTest");

            var updateIngredientCommand = new UpdateIngredientCommand(existingIngredient.Id, existingIngredient.Name);

            var updateProjectCommandHandler = new UpdateIngredientsCommandHandler(ingredientRepository, mapper.Object);
            var id = await updateProjectCommandHandler.Handle(updateIngredientCommand, new CancellationToken());

            Assert.True(id >= Guid.Empty);
        }

    }
}
