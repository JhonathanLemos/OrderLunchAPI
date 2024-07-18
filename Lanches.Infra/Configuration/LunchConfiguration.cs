using Lanches.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lanches.Infraestructure.Configuration
{
    public class LunchConfiguration : IEntityTypeConfiguration<Lunch>
    {
        public void Configure(EntityTypeBuilder<Lunch> builder)
        {
            builder
               .HasMany(x => x.Ingredients)
               .WithOne(x => x.Lunch)
               .HasForeignKey(x => x.LunchId);
        }
    }
}
