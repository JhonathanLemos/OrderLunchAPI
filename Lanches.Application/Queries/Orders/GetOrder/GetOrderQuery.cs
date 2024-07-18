using Lanches.Application.Dtos.ViewModels;
using MediatR;

namespace Lanches.Application.Queries.Orders.GetOrder
{
    public class GetOrderQuery : IRequest<OrderViewModel>
    {
        public GetOrderQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
