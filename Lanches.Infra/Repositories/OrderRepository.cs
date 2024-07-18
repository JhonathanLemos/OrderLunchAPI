using Lanches.Core.Entities;
using Lanches.Core.Repositories;
using Lanches.Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Lanches.Infraestructure.Repositories
{
    public class OrderRepository : IOrderRepository

    {
        private readonly LanchesDbContext _context;

        public OrderRepository(LanchesDbContext context)
        {
            _context = context;
        }

        public IQueryable<Order> GetAll()
        {
            var a = _context.Orders.AsNoTracking().AsQueryable();
            return a;
        }

        public async Task<Order> GetOrderById(Guid Id)
        {
            return await _context.Orders.AsNoTracking().Include(x => x.User).Where(x => x.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<Lunch>> GetLunchsByOrderId(Guid id)
        {
            return await _context.LunchItem.AsNoTracking()
                 .Include(x => x.Lunch)
                 .Where(x => x.OrderId == id).Select(x => x.Lunch).ToListAsync();
        }


        public async Task<Guid> Update(Order Entity)
        {
            _context.Update(Entity);
            await _context.SaveChangesAsync();
            return Entity.Id;
        }

        public async Task<Guid> CreateAsync(Order Entity)
        {
            await _context.AddAsync(Entity);
            await _context.SaveChangesAsync();
            return Entity.Id;
        }

        public async Task Delete(Order Entity)
        {
            _context.Remove(Entity);
            await _context.SaveChangesAsync();
        }
    }
}
