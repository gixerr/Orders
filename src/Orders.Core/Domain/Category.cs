using System;

namespace Orders.Core.Domain
{
    public class Category : Entity
    {
        public string Name { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        protected Category()
        {
        }

        public Category(string name)
        {
            this.Name = Validate(name);
            this.CreatedAt = DateTime.UtcNow;
        }        
    }
}   