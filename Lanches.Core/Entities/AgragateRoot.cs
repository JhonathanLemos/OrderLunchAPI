using Lanches.Core.Events;

namespace Lanches.Core.Entities
{
    public class AgragateRoot : IEntityBase
    {
        private List<IDomainEvent> _events = new List<IDomainEvent>();

        public IEnumerable<IDomainEvent> Events => _events;

        public Guid Id { get; protected set; }

        public void SetId(Guid id)
        {
            Id = id;
        }
        protected void AddEvent(IDomainEvent @event)
        {
            _events.Add(@event);
        }

        public void AddEventsToModel(IEnumerable<IDomainEvent> model)
        {
            foreach (var @event in model)
            {
                _events.Add(@event);
            }
        }
    }
}
