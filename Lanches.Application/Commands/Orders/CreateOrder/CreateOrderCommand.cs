using Lanches.Core.Entities;
using MediatR;

namespace Lanches.Application.Commands.Orders.CreateOrder
{
    public class CreateOrderCommand : AgragateRoot, IRequest<Guid>
    {
        public string Name { get; set; }
        public DateTime OrderDate { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public string? UserId { get; private set; }
        public List<Guid> Lunchs { get; set; }

        public void SetUserId(string? userId)
        {
            UserId = userId;
        }
    }
}
