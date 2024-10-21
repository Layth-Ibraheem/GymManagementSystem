using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Domain.Common
{
    public abstract class Entity
    {
        protected readonly List<IDomainEvent> _domainEvents = [];
        protected Entity() { }
        public List<IDomainEvent> PopDomainEvents()
        {
            var copy = _domainEvents.ToList();
            _domainEvents.Clear();
            return copy;
        }
    }
}
