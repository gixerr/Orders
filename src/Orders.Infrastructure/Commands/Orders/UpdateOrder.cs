using System;
using System.Collections.Generic;
using Orders.Core.Domain;
using Orders.Infrastructure.Commands.Interfaces;

namespace Orders.Infrastructure.Commands.Orders
{
    public class UpdateOrder : ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }
        public IEnumerable<OrderItem> Items { get; set; }
        
    }
}