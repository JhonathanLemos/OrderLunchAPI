using Lanches.Application.Commands.Orders.CreateOrder;

namespace Lanches.Application.ChainOfResponsibility
{
    public interface IOrderOrchestrator
    {
        Task<Guid> Execute(CreateOrderCommand request);
    }
}
