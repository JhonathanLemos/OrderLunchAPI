using AutoMapper;
using Lanches.Application.Commands.Orders.UpdateOrder;
using Lanches.Core.Entities;
using Lanches.Core.Repositories;

namespace Lanches.Application.ChainOfResponsibility
{
    public class UpdateOrderHandler : UpdateOrderHandlerBase, IUpdateOrderHandler
    {
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;

        public UpdateOrderHandler(IOrderRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override async Task<bool> Handle(UpdateOrderCommand request)
        {
            var order = await _repository.GetOrderById(request.Id);
            if (order is null)
                throw new Exception($"Pedido com id {request.Id}");

            _mapper.Map(order, request);
            Guid orderId = await _repository.Update(order);
            request.SetId(orderId);
            request.AddEventsToModel(order.Events);
            return await base.Handle(request);
        }
    }
}
