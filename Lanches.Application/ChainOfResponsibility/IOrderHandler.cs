
using Lanches.Application.Commands.Orders.CreateOrder;

namespace Lanches.Application.ChainOfResponsibility
{
    public interface IOrderHandler
    {
        Task<bool> Handle(CreateOrderCommand request);
        IOrderHandler SetNext(IOrderHandler handler);
    }
}
