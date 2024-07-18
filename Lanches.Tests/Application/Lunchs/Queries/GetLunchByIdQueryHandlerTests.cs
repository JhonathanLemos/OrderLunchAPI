using Blogger.IntegrationTests.Fixtures;
using Lanches.Application.Queries.Lunchs.GetLunch;
using Lanches.Core.Entities;
using Lanches.Infraestructure.Repositories;
using Lanches.Tests.Application.Lunchs;

namespace Lanches.Test.Application.Lunches.Queries
{
    public class GetLunchByIdQueryHandlerTests : IClassFixture<LanchesDbContextFixture>
    {
        private readonly LanchesDbContextFixture _context;

        public GetLunchByIdQueryHandlerTests(LanchesDbContextFixture context)
        {
            _context = context;
        }

        [Fact]
        public async Task GetLunchHandler_WhenCalled_ReturnsValidLunch()
        {
            using var context = _context.BuildDbContext("TestDb");
            var lunchRepository = new GenericRepository<Lunch>(context);
            var lunchId = await new LunchFactoryItems(context).CreateItem();
            var newLunch = LunchFactory.GetLunch(lunchId);
            var getLunchQueryHandler = new GetLunchQueryHandler(lunchRepository);
            var list = lunchRepository.GetAll();
            var lunchViewModel = await getLunchQueryHandler.Handle(newLunch, new CancellationToken());

            Assert.True(lunchViewModel.Id >= Guid.Empty);
            Assert.NotNull(lunchViewModel);
        }
    }
}
