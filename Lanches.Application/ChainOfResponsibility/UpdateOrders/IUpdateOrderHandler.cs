using Lanches.Application.Commands.Orders.UpdateOrder;

namespace Lanches.Application.ChainOfResponsibility
{
    public interface IUpdateOrderHandler
    {
        Task<bool> Handle(UpdateOrderCommand request);
        IUpdateOrderHandler SetNext(IUpdateOrderHandler handler);
    }
}
