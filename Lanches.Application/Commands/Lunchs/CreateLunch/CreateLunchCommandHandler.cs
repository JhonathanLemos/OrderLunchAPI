using Lanches.Application.Services;
using MediatR;


namespace Lanches.Application.Commands.Lunchs.CreateLunch
{
    public class CreateLunchCommandHandler : IRequestHandler<CreateLunchCommand, Guid>
    {
        private readonly ICreateLunchService _lunchService;

        public CreateLunchCommandHandler(ICreateLunchService lunchService)
        {
            _lunchService = lunchService;
        }

        public async Task<Guid> Handle(CreateLunchCommand request, CancellationToken cancellationToken)
        {
            return await _lunchService.CreateLunchAsync(request, cancellationToken);
        }
    }
}
