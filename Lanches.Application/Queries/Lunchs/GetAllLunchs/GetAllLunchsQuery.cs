using Lanches.Application.Dtos;
using Lanches.Application.Dtos.ViewModels;
using Lanches.Core.Models;
using MediatR;

namespace Lanches.Application.Queries.Lunchs.GetAllLunchs
{
    public class GetAllLunchsQuery : IRequest<PaginationResult<LunchViewModel>>
    {
        public GetAllLunchsQuery(GetAll query)
        {
            Query = query;
        }
        public GetAll Query { get; set; }
    }
}
