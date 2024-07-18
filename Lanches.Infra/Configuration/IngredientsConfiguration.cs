using Lanches.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lanches.Infraestructure.Configuration
{
    public class IngredientsConfiguration : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            builder
                 .HasIndex(c => c.Name)
                .IsUnique();

            builder
                .HasMany(x => x.Lunchs)
                .WithOne(x => x.Ingredient)
                .HasForeignKey(x => x.IngredientId);
        }
    }
}
