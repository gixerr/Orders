using System;

namespace Orders.Core.Domain
{
    public class OrderItem
    {
        public Guid Id { get; set;}
        public string Name { get; set; }
        public Category Category { get; set; }
        public Counter Counter { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice =>  Counter.Value * UnitPrice;

        public OrderItem(PreOrderItem preOrderItem)
        {
            this.Id = preOrderItem.Id;
            this.Name = preOrderItem.Name;
            this.Category = preOrderItem.Category;
            this.Counter = preOrderItem.Counter;
            this.UnitPrice = preOrderItem.UnitPrice;
        }

        public OrderItem()
        {
            Counter = new Counter();
        }
    }
}