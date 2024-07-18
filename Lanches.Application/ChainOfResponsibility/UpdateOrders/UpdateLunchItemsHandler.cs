using Lanches.Application.Commands.Orders.UpdateOrder;
using Lanches.Core.Entities;
using Lanches.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Lanches.Application.ChainOfResponsibility
{
    public class UpdateLunchItemsHandler : UpdateOrderHandlerBase
    {
        private readonly IGenericRepository<LunchItem> _lunchItemRepository;
        private readonly IGenericRepository<Lunch> _lunchRepository;
        private readonly IOrderRepository _orderRepository;

        public UpdateLunchItemsHandler(IGenericRepository<LunchItem> lunchItemRepository, IOrderRepository orderRepository, IGenericRepository<Lunch> lunchRepository)
        {
            _lunchItemRepository = lunchItemRepository;
            _orderRepository = orderRepository;
            _lunchRepository = lunchRepository;
        }

        public override async Task<bool> Handle(UpdateOrderCommand request)
        {
            var lunchsList = await _orderRepository.GetAll().Where(x => x.Id == request.Id).Select(x => x.Lunchs).FirstOrDefaultAsync();
            await _lunchItemRepository.DeleteRange(lunchsList);

            foreach (var item in request.Lunchs)
            {
                if (await _lunchRepository.GetByIdAsync(item) is null)
                    throw new Exception($"Lunch with ID {item} not found.");

                var lunchItem = new LunchItem(request.Id, item, request.Quantity);
                await _lunchItemRepository.CreateAsync(lunchItem);
            }

            return await base.Handle(request);
        }
    }

}
