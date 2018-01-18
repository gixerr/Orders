using System;

namespace Orders.Core.Domain
{
    public abstract class Entity
    {
        public Guid Id {get; private set;}

        protected Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}