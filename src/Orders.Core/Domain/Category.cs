using System;
using Orders.Core.Exceptions;

namespace Orders.Core.Domain
{
    public class Category: Entity
    {
        public string Name { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        protected Category()
        {
        }

        public Category(string name) : base()
        {
            this.Name = Validate(name);
            this.CreatedAt = DateTime.UtcNow;

        }        

    }
}