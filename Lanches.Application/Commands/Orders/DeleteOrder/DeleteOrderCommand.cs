using MediatR;

namespace Lanches.Application.Commands.Orders.DeleteOrder
{
    public class DeleteOrderCommand : IRequest<Guid?>
    {
        public DeleteOrderCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
