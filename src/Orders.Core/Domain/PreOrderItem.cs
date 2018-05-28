using System;

namespace Orders.Core.Domain
{
    public class PreOrderItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public Counter Counter { get; set; }
        public decimal UnitPrice { get; set; }

        public PreOrderItem()
        {
            this.Counter = new Counter();
        }
    }
}