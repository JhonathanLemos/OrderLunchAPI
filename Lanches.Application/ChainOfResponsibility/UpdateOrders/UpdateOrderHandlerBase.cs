using Lanches.Application.Commands.Orders.UpdateOrder;

namespace Lanches.Application.ChainOfResponsibility
{
    public class UpdateOrderHandlerBase : IUpdateOrderHandler
    {
        private IUpdateOrderHandler? _nextHandler;
        public virtual async Task<bool> Handle(UpdateOrderCommand request)
        {
            if (_nextHandler == null)
                return true;

            var result = await _nextHandler.Handle(request);

            return result;
        }

        public IUpdateOrderHandler SetNext(IUpdateOrderHandler step)
        {
            _nextHandler = step;

            return step;
        }
    }
}
