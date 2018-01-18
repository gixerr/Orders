using System;
using Orders.Core.Exceptions;

namespace Orders.Core.Domain
{
    public class Item : Entity
    {
        public string Name { get; protected set; }
        public Category Category { get; protected set; }
        public Counter Counter { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        protected Item()
        {
        }

        public Item(string name, Category category) : base()
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                throw new OrderException(ErrorCode.empty_item_name, "Item name can not be empty.");
            }
            this.Name = Name;
            this.Category = category;
            this.Counter = new Counter();
            this.CreatedAt = DateTime.UtcNow;
        }

    }
}