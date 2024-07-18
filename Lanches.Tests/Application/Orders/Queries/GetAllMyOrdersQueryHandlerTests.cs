using Blogger.IntegrationTests.Fixtures;
using Lanches.Application.Queries.Orders.GetOrder;
using Lanches.Core.Entities;
using Lanches.Core.Repositories;
using Lanches.Infraestructure.Context;
using Lanches.Tests.Application.Orders;
using Lanches.Tests.Application.Users;
using Moq;

namespace Lanches.Test.Application.Orders.Queries
{
    public class GetAllMyOrdersQueryHandlerTests : IClassFixture<LanchesDbContextFixture>
    {
        private readonly LanchesDbContext _context;
        private readonly LanchesDbContextFixture _fixture;

        public GetAllMyOrdersQueryHandlerTests(LanchesDbContextFixture fixture)
        {
            _fixture = fixture;
            _context = _fixture.BuildDbContext("TestDb");
        }

        [Fact]
        public async Task GetAllMyOrdersHandler_WhenCalled_ReturnsAllOrders()
        {
            var orderRepository = new Mock<IOrderRepository>();
            //await new UserFactorItems(_context).CreateItem();
            var ordersList = new List<Order>
            {
                new Order("Lanche 1", "1", DateTime.Now, 10),
                new Order("Lanche 2", "1", DateTime.Now, 10),
                new Order("Lanche 3", "1", DateTime.Now, 10),
            };

            orderRepository.Setup(repo => repo.GetAll())
                          .Returns(ordersList.AsQueryable());
            var orderQuery = OrderFactory.GetAllMyOrders();

            var getAllIngredientQueryHandler = new GetAllMyOrderQueryHandler(orderRepository.Object);

            var ingredientViewModel = await getAllIngredientQueryHandler.Handle(orderQuery, new CancellationToken());
            Assert.Equal(ingredientViewModel.Count, 3);
            orderRepository.Verify(x => x.GetAll(), Times.Once);
        }
    }
}
