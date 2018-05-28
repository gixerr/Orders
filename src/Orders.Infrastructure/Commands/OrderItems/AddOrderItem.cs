using System;
using Orders.Infrastructure.Commands.Interfaces;

namespace Orders.Infrastructure.Commands.OrderItems
{
    public class AddOrderItem : ICommand
    {
        public Guid OrderId { get; set; }
        public Guid ItemId { get; set; }
    }
}