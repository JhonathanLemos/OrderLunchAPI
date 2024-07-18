using Lanches.Application.Commands.Orders.CreateOrder;
using Lanches.Infraestructure.MessageBus;

namespace Lanches.Application.ChainOfResponsibility
{
    public class PublishEventsHandler : OrderHandlerBase
    {
        private readonly IMessageBusClient _messageBus;

        public PublishEventsHandler(IMessageBusClient messageBus)
        {
            _messageBus = messageBus;
        }

        public override async Task<bool> Handle(CreateOrderCommand request)
        {
            foreach (var @event in request.Events)
            {
                var routingKey = @event.GetType().Name.ToDashCase();
                _messageBus.Publish(@event, routingKey, "order-service");
            }

            return await base.Handle(request);
        }
    }

}
