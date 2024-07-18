using MediatR;

namespace Lanches.Application.Commands.Orders.DeleteLunch
{
    public class DeleteLunchCommand : IRequest<Guid?>
    {
        public DeleteLunchCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
