using System;
using System.Collections.Generic;
using System.Linq;
using Orders.Core.Domain.Extensions;
using Orders.Core.Exceptions;

namespace Orders.Core.Domain
{
    public class Order : Entity
    {
        public string Name { get; protected set; }
        public DateTime CreatedAt { get; }
        public Status Status { get; protected set; }
        public IEnumerable<OrderItem> Items { get; set; }

        protected Order() { }

        private Order(PreOrder preOrder) : this(preOrder.Name)
        {
            this.Items = preOrder.Items.Select(x => new OrderItem(x));
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
    }
}

//TODO Clear