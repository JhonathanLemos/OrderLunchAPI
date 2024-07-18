using Lanches.Application.Queries.Orders.GetOrder;
using Lanches.Core.Entities;
using Lanches.Core.Repositories;
using Moq;
using StackExchange.Redis;

namespace Lanches.Test.Application.Orders.Queries
{
    public class GetOrderByIdQueryHandlerTests
    {
        [Fact]
        public async Task GetOrderByIdHandler_WhenCalled_ReturnsValidOrder()
        {
            var order = new Lanches.Core.Entities.Order("Lanche 1", "1", DateTime.Now, 10);
            var orderRepository = new Mock<IOrderRepository>();
            orderRepository.Setup(repo => repo.GetOrderById(order.Id))
                .ReturnsAsync(order);

            var getOrderQuery = new GetOrderQuery(order.Id);
            var getOrderQueryHandler = new GetOrderQueryHandler(orderRepository.Object);

            var orderViewModel = await getOrderQueryHandler.Handle(getOrderQuery, new CancellationToken());

            Assert.NotNull(orderViewModel);
            Assert.Equal(order.Id, orderViewModel.Id);
            orderRepository.Verify(x => x.GetOrderById(order.Id), Times.Once); // Corrigido: Verificação do ID específico
        }

    }
}
