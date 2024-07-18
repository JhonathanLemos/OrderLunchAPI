using Lanches.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lanches.Infraestructure.Configuration
{
    public class OrdersConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasMany(x => x.Lunchs)
                .WithOne(x => x.Order);

            builder.HasOne(x => x.User)
                .WithMany(x => x.Orders);

        }
    }
}
