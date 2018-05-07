using System;

namespace Orders.Core.Domain
{
    public class OrderItem
    {
        public Guid Id { get; }
        public string Name { get; }
        public Category Category { get; }
        public Counter Counter { get; }
        public decimal UnitPrice { get; }
        public decimal TotalPrice { get; }

        public OrderItem(PreOrderItem preOrderItem)
        {
            this.Id = preOrderItem.Id;
            this.Name = preOrderItem.Name;
            this.Category = preOrderItem.Category;
            this.Counter = preOrderItem.Counter;
            this.UnitPrice = preOrderItem.UnitPrice;
            this.TotalPrice = preOrderItem.TotalPrice;
        }
    }
}