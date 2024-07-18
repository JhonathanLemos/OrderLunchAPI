using Lanches.Core.Entities;

namespace Lanches.Core.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(Guid id);
        Task<List<User>> GetAllAsync();
        Task<string> AddAsync(User user);
    }
}
