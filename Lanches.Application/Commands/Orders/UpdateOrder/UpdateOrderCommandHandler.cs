using Lanches.Application.ChainOfResponsibility;
using MediatR;

namespace Lanches.Application.Commands.Orders.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Guid>
    {
        private readonly IUpdateOrderOrchestrator _orchestrator;

        public UpdateOrderCommandHandler(IUpdateOrderOrchestrator orchestrator)
        {
            _orchestrator = orchestrator;
        }

        public async Task<Guid> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            return await _orchestrator.Execute(request);
        }
    }
}
