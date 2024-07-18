
using Lanches.Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Blogger.IntegrationTests.Fixtures;

public class LanchesDbContextFixture : EfDatabaseBaseFixture<LanchesDbContext>
{
    protected override LanchesDbContext BuildDbContext(DbContextOptions<LanchesDbContext> options)
    {
        return new LanchesDbContext(options);
    }
}
