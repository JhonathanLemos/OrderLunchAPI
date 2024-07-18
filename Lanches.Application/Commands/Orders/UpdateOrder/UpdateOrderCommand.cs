using Lanches.Core.Entities;
using MediatR;

namespace Lanches.Application.Commands.Orders.UpdateOrder
{
    public class UpdateOrderCommand : AgragateRoot, IRequest<Guid>
    {
        public UpdateOrderCommand(Guid id, string name, DateTime orderDate, int quantity, decimal totalPrice, List<Guid> lunchs)
        {
            Id = id;
            Name = name;
            OrderDate = orderDate;
            Quantity = quantity;
            TotalPrice = totalPrice;
            Lunchs = lunchs;
        }

        public string Name { get; set; }
        public DateTime OrderDate { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public string? UserId { get; private set; }
        public List<Guid> Lunchs { get; set; }

        public void SetUserId(string userId)
        {
            UserId = userId;
        }
    }
}
