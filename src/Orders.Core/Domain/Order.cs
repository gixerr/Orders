using System;
using System.Collections.Generic;
using System.Linq;
using Orders.Core.Domain.Extensions;
using Orders.Core.Exceptions;

namespace Orders.Core.Domain
{
    public class Order : Entity
    {
        private List<OrderItem> _items = new List<OrderItem>();
        public string Name { get; protected set; }
        public DateTime CreatedAt { get; }
        public Status Status { get; protected set; }
        public IEnumerable<OrderItem> Items => _items;

        protected Order() { }

        private Order(PreOrder preOrder) : this(preOrder.Name)
        {
            _items = preOrder.Items.Select(x => new OrderItem(x)).ToList();
        }
        private Order(string name, Status status = Status.Purchased)
        {
            this.Name = Validate(name);
            this.CreatedAt = DateTime.UtcNow;
            this.Status = status;
        }

        public static Order Empty(string name)
            => new Order(name);
        
        public static Order FromPreOrder(PreOrder preOrder)
            => new Order(preOrder);

        public void SetName(string name)
            => Name = Validate(name);

        public void SetStatus(Status status)
            => Status = status;
        
        public void AddOrderItem(OrderItem orderItem)
            => _items.Add(orderItem);

        public void RemoveOrderItem(OrderItem orderItem)
            => _items.Remove(orderItem);

        public void ClearOrderItems()
            => _items.Clear();
    }
}