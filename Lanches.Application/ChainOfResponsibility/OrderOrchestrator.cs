using Lanches.Application.Commands.Orders.CreateOrder;
using Lanches.Core.Entities;
using Lanches.Core.Repositories;
using Lanches.Infraestructure.MessageBus;

namespace Lanches.Application.ChainOfResponsibility
{
    public class OrderOrchestrator : IOrderOrchestrator
    {
        private readonly IMessageBusClient _messageBus;
        private readonly IOrderRepository _orderRepository;
        private readonly IGenericRepository<LunchItem> _lunchItemRepository;
        private readonly IGenericRepository<Lunch> _lunchRepository;
        public OrderOrchestrator(IMessageBusClient messageBus, IOrderRepository orderRepository, IGenericRepository<LunchItem> lunchItemRepository, IGenericRepository<Lunch> lunchRepository)
        {
            _messageBus = messageBus;
            _orderRepository = orderRepository;
            _lunchItemRepository = lunchItemRepository;
            _lunchRepository = lunchRepository;
        }
        public async Task<Guid> Execute(CreateOrderCommand createOrderCommand)
        {
            var createNewOrderHandler = new CreateNewOrderHandler(_orderRepository);
            var createLunchItemsHandler = new CreateLunchItemsHandler(_lunchItemRepository, _lunchRepository);
            var publishEventsHandler = new PublishEventsHandler(_messageBus);

            createNewOrderHandler
                .SetNext(createLunchItemsHandler)
                .SetNext(publishEventsHandler);

            var result = await createNewOrderHandler.Handle(createOrderCommand);

            return result ? createOrderCommand.Id : Guid.Empty;
        }
    }
}
