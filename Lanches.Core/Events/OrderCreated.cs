using Lanches.Core.Entities;

namespace Lanches.Core.Events
{
    public class OrderCreated : IDomainEvent
    {
        public OrderCreated(Guid id, string? userId, string name, decimal totalPrice, DateTime createdAt)
        {
            Id = id;
            UserId = userId;
            Name = name;
            CreatedAt = createdAt;
            TotalPrice = totalPrice;
        }

        public Guid Id { get; }
        public string? UserId { get; }
        public string? Name { get; }
        public DateTime CreatedAt { get; }
        public decimal TotalPrice { get; }
    }
}