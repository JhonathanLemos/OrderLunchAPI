using AutoMapper;
using Lanches.Application.Dtos.ViewModels;
using Lanches.Core.Models;
using Lanches.Core.Repositories;
using Lanches.Infra.CacheStorage;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Lanches.Application.Queries.Orders.GetAllOrder
{
    public class GetAllOrderQueryHandler : IRequestHandler<GetAllOrderQuery, PaginationResult<OrderViewModel>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICacheService _cache;
        private readonly IMapper _mapper;
        private const string CACHE_KEY = "Pedidos";

        public GetAllOrderQueryHandler(IOrderRepository orderRepository, ICacheService cache, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _cache = cache;
            _mapper = mapper;
        }

        public async Task<PaginationResult<OrderViewModel>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = $"{CACHE_KEY}_{request.Query.Page}_{request.Query.PageSize}";
            var paginationResult = await _cache.GetAsync<PaginationResult<OrderViewModel>>(cacheKey);

            if (paginationResult is null || paginationResult.Data.Count == 0)
            {
                var orders = await _orderRepository.GetAll().Include(x => x.User).GetPaged(request.Query.Page, request.Query.PageSize);
                paginationResult = orders.ReplaceData(_mapper.Map<List<OrderViewModel>>(orders.Data));

                await _cache.SetAsync(cacheKey, paginationResult);
            }

            return paginationResult;
        }

    }
}
