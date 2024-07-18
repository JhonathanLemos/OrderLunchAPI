using Lanches.Application.Commands.Orders.DeleteOrder;
using Lanches.Core.Entities;
using Lanches.Core.Repositories;
using Moq;

namespace Lanches.Tests.Application.Orders.Commands
{
    public class DeleteOrderCommandHandlerTests
    {
        [Fact]
        public async Task DeleteOrderHandler_WhenCalled_ReturnsValidId()
        {
            var order = new Order("Pedido 1", "1", DateTime.Now, 10);
            var orderRepository = new Mock<IOrderRepository>();
            orderRepository.Setup(repo => repo.GetOrderById(order.Id)).ReturnsAsync(order);
            var newOrder = OrderFactory.GetOrderToDelete(order.Id);

            var deleteProjectCommandHandler = new DeleteOrderCommandHandler(orderRepository.Object);

            var id = await deleteProjectCommandHandler.Handle(newOrder, new CancellationToken());

            Assert.True(id >= Guid.Empty);

            orderRepository.Verify(x => x.Delete(It.IsAny<Order>()), Times.Once);
        }
    }
}
