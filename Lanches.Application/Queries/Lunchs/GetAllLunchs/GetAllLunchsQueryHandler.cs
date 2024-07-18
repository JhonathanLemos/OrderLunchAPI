using Lanches.API.Extensions;
using Lanches.Application.Dtos.ViewModels;
using Lanches.Core.Entities;
using Lanches.Core.Models;
using Lanches.Core.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Lanches.Application.Queries.Lunchs.GetAllLunchs
{
    public class GetAllLunchsQueryHandler : IRequestHandler<GetAllLunchsQuery, PaginationResult<LunchViewModel>>
    {
        private readonly IGenericRepository<Lunch> _repository;
        public GetAllLunchsQueryHandler(IGenericRepository<Lunch> repository)
        {
            _repository = repository;
        }

        public async Task<PaginationResult<LunchViewModel>> Handle(GetAllLunchsQuery request, CancellationToken cancellationToken)
        {
            var lunchs = await _repository.GetAll()
                .WhereIf(string.IsNullOrEmpty(request.Query.Search), x => x.Name.Contains(request.Query.Search))
                .Include(x => x.Ingredients)
                    .ThenInclude(x => x.Ingredient)
                .GetPaged(request.Query.Page, request.Query.PageSize);

            return lunchs.ReplaceData(lunchs.Data.Select(LunchViewModel.FromEntity).ToList());
        }
    }
}
