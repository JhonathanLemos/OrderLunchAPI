using Lanches.Application.Dtos.ViewModels;
using MediatR;

namespace Lanches.Application.Queries.Lunchs.GetLunch
{
    public class GetLunchQuery : IRequest<LunchViewModel>
    {
        public GetLunchQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
