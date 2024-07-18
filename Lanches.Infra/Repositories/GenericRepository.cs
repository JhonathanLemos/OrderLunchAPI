using Lanches.Core.Entities;
using Lanches.Core.Repositories;
using Lanches.Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Lanches.Infraestructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly LanchesDbContext _DbContext;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(LanchesDbContext dbContext)
        {
            _DbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public async Task<Guid> CreateAsync(T Entity)
        {
            await _dbSet.AddAsync(Entity);
            await _DbContext.SaveChangesAsync();
            return Entity.Id;
        }

        public async Task Delete(T Entity)
        {
            _DbContext.Remove(Entity);
            await _DbContext.SaveChangesAsync();
        }

        public async Task DeleteRange(IEnumerable<T> entities)
        {
            _DbContext.RemoveRange(entities);
            await _DbContext.SaveChangesAsync();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsNoTracking().AsQueryable();
        }

        public async Task<T> GetByIdAsync(Guid Id)
        {
            return await _dbSet.FindAsync(Id);
        }

        public async Task<Guid> Update(T Entity)
        {
            _dbSet.Update(Entity);
            await _DbContext.SaveChangesAsync();
            return Entity.Id;
        }
    }
}
