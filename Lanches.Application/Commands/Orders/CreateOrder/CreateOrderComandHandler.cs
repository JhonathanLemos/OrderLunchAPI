using Lanches.Application.ChainOfResponsibility;
using MediatR;

namespace Lanches.Application.Commands.Orders.CreateOrder
{
    public class CreateOrderComandHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IOrderOrchestrator _orchestrator;

        public CreateOrderComandHandler(IOrderOrchestrator orchestrator)
        {
            _orchestrator = orchestrator;
        }

        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            return await _orchestrator.Execute(request);
        }
    }
}
