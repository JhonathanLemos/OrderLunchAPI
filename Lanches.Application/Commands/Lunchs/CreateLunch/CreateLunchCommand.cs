using Lanches.Core.Entities;
using MediatR;

namespace Lanches.Application.Commands.Lunchs.CreateLunch
{
    public class CreateLunchCommand : BaseEntity, IRequest<Guid>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public List<Guid> Ingredients { get; set; }
    }
}
