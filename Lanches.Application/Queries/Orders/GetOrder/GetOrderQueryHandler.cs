using Lanches.Application.Dtos.ViewModels;
using Lanches.Core.Repositories;
using MediatR;

namespace Lanches.Application.Queries.Orders.GetOrder
{
    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OrderViewModel>
    {
        private readonly IOrderRepository _orderRepository;
        public GetOrderQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<OrderViewModel> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderById(request.Id);
            return order == null ? null : OrderViewModel.FromEntity(order);
        }
    }
}
