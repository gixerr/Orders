using System;
using Orders.Core.Exceptions;

namespace Orders.Core.Domain
{
    public class Item : Entity
    {
        public string Name { get; protected set; }
        public Category Category { get; protected set; }
        public Counter Counter { get; protected set; } // TODO ICounter?
        public decimal Price { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        protected Item()
        {
        }

        public Item(string name, decimal price, Category category) : base()
        {
            this.Name = Validate(name);
            this.Category = Validate(category);
            this.Counter = new Counter();
            this.Price = Validate(price);
            this.CreatedAt = DateTime.UtcNow;
        }

        private decimal Validate(decimal price)
        {
            if (price <= 0)
            {
                throw new OrdersException(ErrorCode.invalid_price, $"Given price '{price}' is invalid. Price must be greater then 0.");
            }

            return price;
        }

        private Category Validate(Category category)
        {
            if (category is null)
            {
                throw new OrdersException(ErrorCode.category_not_found, "Category cannot be empty.");
            }

            return category;
        }
    }
}