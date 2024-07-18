using Lanches.Application.Commands.Orders.UpdateOrder;

namespace Lanches.Application.ChainOfResponsibility
{
    public interface IUpdateOrderOrchestrator
    {
        Task<Guid> Execute(UpdateOrderCommand request);
    }
}
