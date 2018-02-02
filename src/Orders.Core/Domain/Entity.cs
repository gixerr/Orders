using System;
using Orders.Core.Exceptions;

namespace Orders.Core.Domain
{
    public abstract class Entity
    {
        public Guid Id {get; private set;}

        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        protected string Validate(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new OrdersException(ErrorCode.invalid_name, "Name cannot be empty.");
            }

            return name;
        }
    }
}