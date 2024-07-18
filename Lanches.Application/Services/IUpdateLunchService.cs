using Lanches.Application.Commands.Lunchs.UpdateLunch;

namespace Lanches.Application.Services
{
    public interface IUpdateLunchService
    {
        Task<Guid> UpdateLunchAsync(UpdateLunchCommand request, CancellationToken cancellationToken);
    }
}
