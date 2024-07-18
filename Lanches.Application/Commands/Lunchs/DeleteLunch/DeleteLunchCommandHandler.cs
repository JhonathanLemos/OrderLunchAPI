using Lanches.Core.Entities;
using Lanches.Core.Repositories;
using MediatR;

namespace Lanches.Application.Commands.Orders.DeleteLunch
{
    public class DeleteLunchCommandHandler : IRequestHandler<DeleteLunchCommand, Guid?>
    {
        private readonly IGenericRepository<Lunch> _repository;

        public DeleteLunchCommandHandler(IGenericRepository<Lunch> repository)
        {
            _repository = repository;
        }

        public async Task<Guid?> Handle(DeleteLunchCommand request, CancellationToken cancellationToken)
        {
            var lunch = await _repository.GetByIdAsync(request.Id);
            if (lunch is not null)
                await _repository.Delete(lunch);

            return lunch is null ? null : lunch.Id;
        }
    }
}
