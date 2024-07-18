using Lanches.Application.Commands.Orders.CreateOrder;
using Lanches.Core.Entities;
using Lanches.Core.Repositories;

namespace Lanches.Application.ChainOfResponsibility
{
    public class CreateLunchItemsHandler : OrderHandlerBase
    {
        private readonly IGenericRepository<LunchItem> _lunchItemRepository;
        private readonly IGenericRepository<Lunch> _lunchRepository;

        public CreateLunchItemsHandler(IGenericRepository<LunchItem> lunchItemRepository, IGenericRepository<Lunch> lunchRepository)
        {
            _lunchItemRepository = lunchItemRepository;
            _lunchRepository = lunchRepository;
        }

        public override async Task<bool> Handle(CreateOrderCommand request)
        {
            foreach (var item in request.Lunchs)
            {
                var lunchExists = await _lunchRepository.GetByIdAsync(item);

                if (lunchExists is null)
                    throw new Exception($"Lunch with ID {item} not found.");

                var lunchItem = new LunchItem(request.Id, item, request.Quantity);
                await _lunchItemRepository.CreateAsync(lunchItem);
            }

            return await base.Handle(request);
        }
    }
}
