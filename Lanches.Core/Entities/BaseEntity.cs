namespace Lanches.Core.Entities
{
    public class BaseEntity : IEntityBase
    {
        public Guid Id { get; protected set; }

        public void SetId(Guid id)
        {
            Id = id;
        }
    }
}
