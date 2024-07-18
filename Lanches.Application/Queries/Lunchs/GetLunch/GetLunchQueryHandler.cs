using Lanches.Application.Dtos.ViewModels;
using Lanches.Core.Entities;
using Lanches.Core.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Lanches.Application.Queries.Lunchs.GetLunch
{
    public class GetLunchQueryHandler : IRequestHandler<GetLunchQuery, LunchViewModel>
    {
        private readonly IGenericRepository<Lunch> _repository;

        public GetLunchQueryHandler(IGenericRepository<Lunch> repository)
        {
            _repository = repository;
        }

        public async Task<LunchViewModel> Handle(GetLunchQuery request, CancellationToken cancellationToken)
        {
            var lunch = _repository.GetAll().Where(x => x.Id == request.Id).Include(x => x.Ingredients).ThenInclude(x => x.Ingredient).FirstOrDefault();
            return lunch == null ? null : LunchViewModel.FromEntity(lunch);
        }
    }
}
