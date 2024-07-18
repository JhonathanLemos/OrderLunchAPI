using Lanches.Core.Entities;

namespace Lanches.Core.Repositories
{
    public interface IGenericRepository<T> where T : IEntityBase
    {
        IQueryable<T> GetAll();
        Task<T> GetByIdAsync(Guid Id);
        Task<Guid> CreateAsync(T Entity);
        Task<Guid> Update(T Entity);
        Task Delete(T Entity);
        Task DeleteRange(IEnumerable<T> entities);
    }
}
