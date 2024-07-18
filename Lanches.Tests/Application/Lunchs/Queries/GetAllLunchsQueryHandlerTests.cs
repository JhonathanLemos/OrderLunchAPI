using Blogger.IntegrationTests.Fixtures;
using Lanches.Application.Dtos;
using Lanches.Application.Queries.Lunchs.GetAllLunchs;
using Lanches.Core.Entities;
using Lanches.Infraestructure.Repositories;
using Lanches.Tests.Application.Lunchs;

namespace Lanches.Test.Application.Lunches.Queries
{
    public class GetAllLunchsQueryHandlerTests : IClassFixture<LanchesDbContextFixture>
    {
        private readonly LanchesDbContextFixture _context;

        public GetAllLunchsQueryHandlerTests(LanchesDbContextFixture context)
        {
            _context = context;
        }

        [Fact]
        public async Task GetAllLunchsHandler_WhenCalled_ReturnsAllIngredients()
        {
            using var context = _context.BuildDbContext("TestDb");
            var lunchRepository = new GenericRepository<Lunch>(context);

            var lunchsList = new List<Lunch>
            {
                new Lunch("Lunch 1", 10, "Descrição"),
                new Lunch("Lunch 1", 10, "Descrição"),
                new Lunch("Lunch 1", 10, "Descrição"),
            };

            var lunchQuery = LunchFactory.GetAllLunchs(new GetAll("", 1, 10));

            var getAllIngredientQueryHandler = new GetAllLunchsQueryHandler(lunchRepository);

            var lunchViewModel = await getAllIngredientQueryHandler.Handle(lunchQuery, new CancellationToken());

        }
    }
}
