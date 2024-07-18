using Lanches.Application.Dtos.ViewModels;
using MediatR;

namespace Lanches.Application.Queries.Orders.GetOrder
{
    public class GetAllMyOrderQuery : IRequest<List<OrderViewModel>>
    {
        public GetAllMyOrderQuery(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; set; }
    }
}
