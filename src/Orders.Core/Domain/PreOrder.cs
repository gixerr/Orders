using System;
using System.Collections.Generic;
using Orders.Core.Domain.Extensions;
using Orders.Core.Exceptions;

namespace Orders.Core.Domain
{
    public class PreOrder
    {

        private ISet<PreOrderItem> _items = new HashSet<PreOrderItem>();
        public string Name { get; protected set; }
        public IEnumerable<PreOrderItem> Items => _items;
        private void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new OrdersException(ErrorCode.empty_order_name, "Name cannot be empty.");
            }

            this.Name = name;
        }

        public PreOrder(string name)
        {
            SetName(name);
        }

        public void AddItem(PreOrderItem preOrderItem)
        {
            var itemToAdd = Items.GetItem(preOrderItem.Id);
            if (itemToAdd is null)
            {
                _items.Add(preOrderItem);

                return;
            }
            preOrderItem.Counter.Increase();
        }

        public void RemoveItem(Guid itemId)
        {
            var itemToRemove = _items.GetItem(itemId);
            if (itemToRemove is null)
            {
                throw new OrdersException(ErrorCode.item_not_found, $"Item with given id '{itemId}' not found. Unable to remove nonexiting item.");
            }
            if (itemToRemove.Counter.Value > 1)
            {
                itemToRemove.Counter.Decrease();

                return;
            }
            _items.Remove(itemToRemove);
        }
    }
}