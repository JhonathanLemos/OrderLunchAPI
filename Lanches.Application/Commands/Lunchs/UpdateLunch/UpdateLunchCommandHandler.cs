using Lanches.Application.Services;
using MediatR;

namespace Lanches.Application.Commands.Lunchs.UpdateLunch
{
    public class UpdateLunchCommandHandler : IRequestHandler<UpdateLunchCommand, Guid>
    {
        private readonly IUpdateLunchService _updateLunchService;

        public UpdateLunchCommandHandler(IUpdateLunchService updateLunchService)
        {
            _updateLunchService = updateLunchService;
        }

        public async Task<Guid> Handle(UpdateLunchCommand request, CancellationToken cancellationToken)
        {
            return await _updateLunchService.UpdateLunchAsync(request, cancellationToken);
        }
    }
}
