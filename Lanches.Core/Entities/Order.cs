using Lanches.Core.Enums;
using Lanches.Core.Events;

namespace Lanches.Core.Entities
{
    public class Order : AgragateRoot
    {
        public Order(string name, string? userId, DateTime createdAt, decimal totalPrice)
        {
            Id = Guid.NewGuid();
            Name = name;
            UserId = userId;
            CreatedAt = createdAt;
            TotalPrice = totalPrice;
            Lunchs = new List<LunchItem>();
            Status = OrderStatus.Started;
            AddEvent(new OrderCreated(Id, UserId, Name, TotalPrice, CreatedAt));
        }

        public string Name { get; set; }
        public DateTime CreatedAt { get; private set; }
        public decimal TotalPrice { get; private set; }
        public List<LunchItem> Lunchs { get; private set; }
        public User User { get; private set; }
        public string? UserId { get; private set; }
        public OrderStatus Status { get; private set; }

        public void SetAsCompleted()
        {
            Status = OrderStatus.Completed;
        }
    }
}
