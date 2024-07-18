
using Lanches.Application.Commands.Orders.CreateOrder;

namespace Lanches.Application.ChainOfResponsibility
{
    public class OrderHandlerBase : IOrderHandler
    {
        private IOrderHandler? _nextHandler;
        public virtual async Task<bool> Handle(CreateOrderCommand request)
        {
            if (_nextHandler == null)
                return true;

            var result = await _nextHandler.Handle(request);

            return result;
        }

        public IOrderHandler SetNext(IOrderHandler step)
        {
            _nextHandler = step;

            return step;
        }
    }
}
