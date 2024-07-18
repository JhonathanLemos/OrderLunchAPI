using Lanches.Application.Dtos.ViewModels;
using Lanches.Core.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Lanches.Application.Queries.Orders.GetOrder
{
    public class GetAllMyOrderQueryHandler : IRequestHandler<GetAllMyOrderQuery, List<OrderViewModel>>
    {
        private readonly IOrderRepository _orderRepository;
        public GetAllMyOrderQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<List<OrderViewModel>> Handle(GetAllMyOrderQuery request, CancellationToken cancellationToken)
        {
            var allMyOrders = _orderRepository.GetAll().Include(x => x.Lunchs).ThenInclude(x => x.Lunch).Where(x => x.UserId == request.UserId).ToList();
            return allMyOrders
             .Select(OrderViewModel.FromEntity)
             .ToList();
        }
    }
}
