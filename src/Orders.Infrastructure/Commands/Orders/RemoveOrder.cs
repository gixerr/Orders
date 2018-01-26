using System;

namespace Orders.Infrastructure.Commands.Orders
{
    public class RemoveOrder : ICommand
    {
        public Guid Id { get; set; }
    }
}