using Lanches.Application.Commands.Orders.CreateOrder;
using Lanches.Application.Commands.Orders.DeleteOrder;
using Lanches.Application.Commands.Orders.UpdateOrder;
using Lanches.Application.Dtos;
using Lanches.Application.Queries.Orders.GetAllOrder;
using Lanches.Application.Queries.Orders.GetLunchByOrderIds;
using Lanches.Application.Queries.Orders.GetOrder;
using Lanches.Core.Entities;

namespace Lanches.Tests.Application.Orders
{
    public static class OrderFactory
    {
        public static CreateOrderCommand GetOrderToCreate()
        {
            return new CreateOrderCommand()
            {
                Name = "name",
                Lunchs = new List<Guid>()
            };
        }

        public static UpdateOrderCommand GetOrderToUpdate(Order order, Ingredient ingredient)
        {
            return new UpdateOrderCommand(order.Id, order.Name, DateTime.Now, 1, 10, new List<Guid> { ingredient.Id });
        }

        public static DeleteOrderCommand GetOrderToDelete(Guid id)
        {
            return new DeleteOrderCommand(id);
        }

        public static GetOrderQuery GetOrder(Guid id)
        {
            return new GetOrderQuery(id);
        }

        public static GetLunchByOrderIdQuery GetLunchByOrderIdQuery(Guid id)
        {
            return new GetLunchByOrderIdQuery(id);
        }
        public static GetAllOrderQuery GetAllOrders(GetAll getAll)
        {
            return new GetAllOrderQuery(getAll);
        }

        public static GetAllMyOrderQuery GetAllMyOrders()
        {
            return new GetAllMyOrderQuery("1");
        }
    }
}
