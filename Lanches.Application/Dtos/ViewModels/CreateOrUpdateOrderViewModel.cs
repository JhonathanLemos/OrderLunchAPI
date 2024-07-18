
namespace Lanches.Application.Dtos.ViewModels
{
    public class CreateOrUpdateOrderViewModel
    {
        public CreateOrUpdateOrderViewModel(Guid id, string name, DateTime orderDate, decimal totalPrice, string userId)
        {
            Id = id;
            Name = name;
            OrderDate = orderDate;
            TotalPrice = totalPrice;
            UserId = userId;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public DateTime OrderDate { get; private set; }
        public string? UserId { get; private set; }
        public decimal TotalPrice { get; private set; }

        public void SetUserId(string? userId)
        {
            UserId = userId;
        }
    }


}
