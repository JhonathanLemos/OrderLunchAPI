using Blogger.IntegrationTests.Fixtures;
using Lanches.Application.ChainOfResponsibility;
using Lanches.Application.Commands.Orders.CreateOrder;
using Lanches.Core.Entities;
using Lanches.Infraestructure.Context;
using Lanches.Infraestructure.MessageBus;
using Lanches.Infraestructure.Repositories;
using Lanches.Tests.Application.Lunchs;
using Lanches.Tests.Application.Users;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Lanches.Tests.Application.Orders.Commands
{
    public class CreateOrderCommandHandlerTests : IClassFixture<LanchesDbContextFixture>
    {
        private readonly LanchesDbContextFixture _fixture;
        private readonly Mock<IMessageBusClient> _messageBusMock;
        private readonly LanchesDbContext _context;
        private readonly GenericRepository<LunchItem> _lunchItemRepository;
        private readonly GenericRepository<Lunch> _lunchRepository;
        private readonly OrderRepository _orderRepository;
        private readonly OrderOrchestrator _orderOrchestrator;

        public CreateOrderCommandHandlerTests(LanchesDbContextFixture fixture)
        {
            _fixture = fixture;
            _context = _fixture.BuildDbContext("TestDb");
            _lunchItemRepository = new GenericRepository<LunchItem>(_context);
            _lunchRepository = new GenericRepository<Lunch>(_context);
            _orderRepository = new OrderRepository(_context);
            _messageBusMock = new Mock<IMessageBusClient>();
            _orderOrchestrator = new OrderOrchestrator(_messageBusMock.Object, _orderRepository, _lunchItemRepository, _lunchRepository);
        }

        [Fact]
        public async Task CreateOrderHandler_WhenCalled_ReturnsValidId()
        {
            await SetupTestDataAsync();
            var newOrder = await CreateValidOrderAsync();

            var createOrderCommandHandler = new CreateOrderComandHandler(_orderOrchestrator);

            var id = await createOrderCommandHandler.Handle(newOrder, new CancellationToken());

            Assert.True(id >= Guid.Empty);
        }

        private async Task SetupTestDataAsync()
        {
            await new LunchFactoryItems(_context).CreateItem();
            //await new UserFactorItems(_context).CreateItem();
        }

        private async Task<CreateOrderCommand> CreateValidOrderAsync()
        {
            var newOrder = OrderFactory.GetOrderToCreate();
            var user = await _context.Users.FirstOrDefaultAsync();
            var lunch = await _context.Lunch.FirstOrDefaultAsync();

            newOrder.Lunchs.Add(lunch.Id);
            //newOrder.SetUserId(user.Id);

            return newOrder;
        }
    }
}
