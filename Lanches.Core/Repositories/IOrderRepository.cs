using Lanches.Core.Entities;

namespace Lanches.Core.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> GetOrderById(Guid Id);
        IQueryable<Order> GetAll();
        Task<List<Lunch>> GetLunchsByOrderId(Guid id);
        Task<Guid> Update(Order order);
        Task Delete(Order order);
        Task<Guid> CreateAsync(Order order);
    }
}
