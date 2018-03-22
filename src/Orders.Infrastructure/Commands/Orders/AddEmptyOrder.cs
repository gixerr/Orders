using System;
using Orders.Infrastructure.Commands.Interfaces;

namespace Orders.Infrastructure.Commands.Orders
{
    public class AddEmptyOrder : ICommand
    {
        public string Name { get; set; }
    }
}