using System;
using System.Collections.Generic;
using Orders.Core.Domain.Extensions;
using Orders.Core.Exceptions;

namespace Orders.Core.Domain
{
    public class Order : Entity
    {
        private ISet<Item> _items = new HashSet<Item>();
        public string Name { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public IEnumerable<Item> Items
        {
            get => _items;
            protected set => _items = new HashSet<Item>(value);
        }

        protected Order() { }

        public Order(string name) : base()
        {
            this.Name = Validate(name);
            this.CreatedAt = DateTime.UtcNow;
        }

        public IEnumerable<Item> GetItems(string name)
        {
            var items = Items.GetItems(name);
            if (items is null)
            {
                throw new OrderException(ErrorCode.item_not_found, $"Item with given name '{name}' not found");
            }
            return items;
        }

        public void AddItem(string itemName, decimal price, string categoryName)
        {
            var item = Items.GetItem(itemName, categoryName);
            if (item is null)
            {
                _items.Add(new Item(itemName, price, new Category(categoryName)));
                return;
            }
            item.Counter.Increase();
        }

        public void RemoveItem(string itemName, string categoryName)
        {
            var item = Items.GetItem(itemName, categoryName);
            if (item is null)
            {
                throw new OrderException(ErrorCode.item_not_found, "Unable to remove nonexiting item.");
            }
            if (item.Counter.Value is 1)
            {
                _items.Remove(item);
                return;
            }
            item.Counter.Decrease();
        }
    }
}

//TODO Clear