using System;
using Orders.Infrastructure.Commands.Interfaces;

namespace Orders.Infrastructure.Commands.Items
{
    public class RemoveItem : ICommand
    {
        public Guid Id { get; set; }
    }
}