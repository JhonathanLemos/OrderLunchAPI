using Lanches.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lanches.Infraestructure.Configuration
{
    public class UsersConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder
               .HasMany(x => x.Orders)
               .WithOne(x => x.User)
               .HasForeignKey(x => x.UserId);
        }
    }
}
