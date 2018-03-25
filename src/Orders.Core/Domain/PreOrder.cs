using System;
using System.Collections.Generic;
using Orders.Core.Domain.Extensions;
using Orders.Core.Exceptions;

namespace Orders.Core.Domain
{
    public class PreOrder
    {
        public PreOrder(string name)
        {
            SetName(name);
        }
        private ISet<Item> _items = new HashSet<Item>();
        public string Name { get; protected set; }
        public IEnumerable<Item> Items => _items;
        private void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new OrdersException(ErrorCode.empty_order_name, "Name cannot be empty.");
            }

            this.Name = name;
        }

        public void AddItem(Item item)
        {
            var itemToAdd = Items.GetItem(item.Name, item.Category.Name);
            if (itemToAdd is null)
            {
                _items.Add(item);
                return;
            }
            item.Counter.Increase();
        }
        

    }
}