using System;

namespace Orders.Infrastructure.Commands.PreOrderItems
{
    public class RemoveItem
    {
        public Guid ItemId { get; set; }
        public string OrderName { get; set; }
    }
}