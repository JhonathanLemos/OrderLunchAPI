using AutoMapper;
using Blogger.IntegrationTests.Fixtures;
using Lanches.Application.Dtos;
using Lanches.Application.Queries.Orders.GetAllOrder;
using Lanches.Core.Entities;
using Lanches.Infra.CacheStorage;
using Lanches.Infraestructure.Repositories;
using Lanches.Tests.Application.Orders;
using Moq;

namespace Lanches.Test.Application.Orders.Queries
{
    public class GetAllOrdersQueryHandlerTests : IClassFixture<LanchesDbContextFixture>
    {
        private readonly LanchesDbContextFixture _context;
        public GetAllOrdersQueryHandlerTests(LanchesDbContextFixture context)
        {
            _context = context;
        }
        [Fact]
        public async Task GetAllOrdersHandler_WhenCalled_ReturnsAllOrders()
        {
            using var context = _context.BuildDbContext("TestDb");
            var orderRepository = new OrderRepository(context);

            var cache = new Mock<ICacheService>();
            var mapper = new Mock<IMapper>();
            var ordersList = new List<Order>
            {
                new Order("Lanche 1", "1", DateTime.Now, 10),
                new Order("Lanche 2", "2", DateTime.Now, 10),
                new Order("Lanche 3", "3", DateTime.Now, 10),
            };


            var orderQuery = OrderFactory.GetAllOrders(new GetAll("", 1, 10));

            var getAllIngredientQueryHandler = new GetAllOrderQueryHandler(orderRepository, cache.Object, mapper.Object);

            var ingredientViewModel = await getAllIngredientQueryHandler.Handle(orderQuery, new CancellationToken());

        }
    }
}
