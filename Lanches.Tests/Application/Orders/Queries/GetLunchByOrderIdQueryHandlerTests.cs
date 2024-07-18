using Lanches.Application.Queries.Orders.GetLunchByOrderIds;
using Lanches.Core.Entities;
using Lanches.Core.Repositories;
using Lanches.Tests.Application.Orders;
using Moq;

namespace Lanches.Test.Application.Orders.Queries
{
    public class GetLunchByOrderIdQueryHandlerTests
    {
        [Fact]
        public async Task GetLunchByOrderIdHandler_WhenCalled_ReturnsValidOrder()
        {
            var orderId = Guid.NewGuid();
            var orderRepository = new Mock<IOrderRepository>();
            var lunch = new List<Lunch>
            {
                new Lunch("Name", 10, "Description")
            };

            orderRepository.Setup(repo => repo.GetLunchsByOrderId(orderId)).ReturnsAsync(lunch);
            var query = OrderFactory.GetLunchByOrderIdQuery(orderId);

            var handler = new GetLunchByOrderIdQueryHandler(orderRepository.Object);

            var orderViewModel = await handler.Handle(query, new CancellationToken());

            Assert.True(orderViewModel.Count >= 0);
            Assert.NotNull(orderViewModel);
        }
    }
}
