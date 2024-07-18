using Lanches.Infraestructure.Context;

namespace Lanches.Tests.Application.Ingredients
{
    public class OrderFactoryItems
    {
        private readonly LanchesDbContext _context;

        public OrderFactoryItems(LanchesDbContext context)
        {
            _context = context;
        }
        public async Task CreateItem()
        {

        }
    }
}
