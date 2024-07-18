using Lanches.Core.Repositories;
using MediatR;

namespace Lanches.Application.Commands.Orders.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Guid?>
    {
        private readonly IOrderRepository _repository;

        public DeleteOrderCommandHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid?> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _repository.GetOrderById(request.Id);
            if (order is not null)
                await _repository.Delete(order);

            return order is null ? null : order.Id;
        }
    }
}
