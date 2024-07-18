using AutoMapper;
using Lanches.Application.Commands.Orders.UpdateOrder;
using Lanches.Core.Entities;
using Lanches.Core.Repositories;
using Lanches.Infraestructure.MessageBus;

namespace Lanches.Application.ChainOfResponsibility
{
    public class UpdateOrderOrchestrator : IUpdateOrderOrchestrator
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IGenericRepository<LunchItem> _lunchItemRepository;
        private readonly IGenericRepository<Lunch> _lunchRepository;
        private readonly IMapper _mapper;
        public UpdateOrderOrchestrator(IOrderRepository orderRepository, IGenericRepository<LunchItem> lunchItemRepository, IGenericRepository<Lunch> lunchRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _lunchItemRepository = lunchItemRepository;
            _lunchRepository = lunchRepository;
            _mapper = mapper;
        }
        public async Task<Guid> Execute(UpdateOrderCommand createOrderCommand)
        {
            var updateOrderHandler = new UpdateOrderHandler(_orderRepository, _mapper);
            var updateLunchItemsHandler = new UpdateLunchItemsHandler(_lunchItemRepository, _orderRepository, _lunchRepository);

            updateOrderHandler
                .SetNext(updateLunchItemsHandler);

            var result = await updateOrderHandler.Handle(createOrderCommand);

            return result ? createOrderCommand.Id : Guid.Empty;
        }
    }
}
