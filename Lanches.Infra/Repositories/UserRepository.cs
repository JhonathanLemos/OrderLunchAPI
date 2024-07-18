using Lanches.Core.Entities;
using Lanches.Core.Repositories;
using Lanches.Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Lanches.Infraestructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LanchesDbContext _dbContext;
        public UserRepository(LanchesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> AddAsync(User Entity)
        {
            await _dbContext.AddAsync(Entity);
            await _dbContext.SaveChangesAsync();
            return Entity.Id;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _dbContext.Users.AsNoTracking().ToListAsync();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _dbContext.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Id == id.ToString());
        }
    }
}
