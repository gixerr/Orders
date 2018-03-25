using System;
using Orders.Infrastructure.Commands.Interfaces;

namespace Orders.Infrastructure.Commands.PreOrderItems
{
    public class AddItem : ICommand
    {
        public Guid ItemId { get; set; }
        public string OrderName { get; set; }
    }
}