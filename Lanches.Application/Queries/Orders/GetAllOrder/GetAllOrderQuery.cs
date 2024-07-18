using Lanches.Application.Dtos;
using Lanches.Application.Dtos.ViewModels;
using Lanches.Core.Models;
using MediatR;

namespace Lanches.Application.Queries.Orders.GetAllOrder
{
    public class GetAllOrderQuery : IRequest<PaginationResult<OrderViewModel>>
    {
        public GetAll Query { get; set; }

        public GetAllOrderQuery(GetAll query)
        {
            Query = query;
        }
    }
}
