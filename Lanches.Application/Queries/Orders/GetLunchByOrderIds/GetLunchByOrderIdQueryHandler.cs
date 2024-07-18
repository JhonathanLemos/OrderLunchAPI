using Lanches.Application.Dtos.ViewModels;
using Lanches.Core.Repositories;
using MediatR;

namespace Lanches.Application.Queries.Orders.GetLunchByOrderIds
{
    public class GetLunchByOrderIdQueryHandler : IRequestHandler<GetLunchByOrderIdQuery, List<LunchViewModel>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetLunchByOrderIdQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<LunchViewModel>> Handle(GetLunchByOrderIdQuery request, CancellationToken cancellationToken)
        {
            var lunchs = await _orderRepository.GetLunchsByOrderId(request.Id);
            return lunchs.Select(LunchViewModel.FromEntity).ToList();
        }
    }
}
