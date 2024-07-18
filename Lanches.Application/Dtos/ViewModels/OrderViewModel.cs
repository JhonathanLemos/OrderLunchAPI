using Lanches.Core.Entities;

namespace Lanches.Application.Dtos.ViewModels
{
    public class OrderViewModel
    {
        public OrderViewModel(Guid id, string name, decimal totalPrice, string? userId)
        {
            Id = id;
            Name = name;
            TotalPrice = totalPrice;
            UserId = userId;
        }

        public static List<OrderViewModel> FromEntities(List<Order> order)
        {
            var newList = new List<OrderViewModel>();
            foreach (var item in order)
            {
                newList.Add(FromEntity(item));
            }

            return newList;
        }

        public static OrderViewModel FromEntity(Order order)
        {
            return new OrderViewModel(order.Id, order.Name, order.TotalPrice, order.UserId);
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public DateTime OrderDate { get; private set; }
        public string? UserId { get; private set; }
        public decimal TotalPrice { get; private set; }
    }


}
