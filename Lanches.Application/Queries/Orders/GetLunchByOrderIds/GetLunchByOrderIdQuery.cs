using Lanches.Application.Dtos.ViewModels;
using MediatR;

namespace Lanches.Application.Queries.Orders.GetLunchByOrderIds
{
    public class GetLunchByOrderIdQuery : IRequest<List<LunchViewModel>>
    {
        public GetLunchByOrderIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
