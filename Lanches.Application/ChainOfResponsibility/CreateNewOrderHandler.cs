using Lanches.Application.Commands.Orders.CreateOrder;
using Lanches.Core.Entities;
using Lanches.Core.Repositories;

namespace Lanches.Application.ChainOfResponsibility
{
    public class CreateNewOrderHandler : OrderHandlerBase, IOrderHandler
    {
        private readonly IOrderRepository _repository;

        public CreateNewOrderHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public override async Task<bool> Handle(CreateOrderCommand request)
        {
            var order = new Order(request.Name, request.UserId, request.OrderDate, request.TotalPrice);
            Guid orderId = await _repository.CreateAsync(order);
            request.SetId(orderId);
            request.AddEventsToModel(order.Events);

            return await base.Handle(request);
        }
    }
}
