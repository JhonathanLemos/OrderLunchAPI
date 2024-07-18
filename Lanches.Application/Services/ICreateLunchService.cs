using Lanches.Application.Commands.Lunchs.CreateLunch;

namespace Lanches.Application.Services
{
    public interface ICreateLunchService
    {
        Task<Guid> CreateLunchAsync(CreateLunchCommand request, CancellationToken cancellationToken);
    }
}
