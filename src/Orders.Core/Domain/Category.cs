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
            if(string.IsNullOrWhiteSpace(name))
            {
                throw new OrderException(ErrorCode.empty_category_name, "Category name can not be empty.");
            }

            this.Name = name;
            this.CreatedAt = DateTime.UtcNow;

        }        

    }
}