namespace Lanches.Core.Entities
{
    public class LunchItem : BaseEntity
    {
        public LunchItem(Guid orderId, Guid lunchId, int quantity)
        {
            OrderId = orderId;
            LunchId = lunchId;
            Quantity = quantity;
        }

        public Order Order { get; private set; }
        public Guid OrderId { get; private set; }
        public Lunch Lunch { get; private set; }
        public Guid LunchId { get; private set; }
        public int Quantity { get; private set; }
    }
}
