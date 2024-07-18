using Blogger.IntegrationTests.Fixtures;
using Lanches.Application.Commands.Orders.DeleteLunch;
using Lanches.Core.Entities;
using Lanches.Infraestructure.Repositories;

namespace Lanches.Tests.Application.Lunchs.Commands
{
    public class DeleteLunchCommandHandlerTests : IClassFixture<LanchesDbContextFixture>
    {
        private readonly LanchesDbContextFixture _context;

        public DeleteLunchCommandHandlerTests(LanchesDbContextFixture context)
        {
            _context = context;
        }

        [Fact]
        public async Task DeleteLunchHandler_WhenCalled_ReturnsValidId()
        {
            using var context = _context.BuildDbContext("TestDb");
            var lunchId = await new LunchFactoryItems(context).CreateItem();
            var lunchRepository = new GenericRepository<Lunch>(context);
            var newLunch = LunchFactory.GetLunchToDelete(lunchId);

            var deleteLunchCommandHandler = new DeleteLunchCommandHandler(lunchRepository);

            var id = await deleteLunchCommandHandler.Handle(newLunch, new CancellationToken());

            Assert.True(id >= Guid.Empty);

        }
    }
}
