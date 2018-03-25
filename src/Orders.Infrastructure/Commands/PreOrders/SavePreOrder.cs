using System.Collections.Generic;
using Orders.Infrastructure.Commands.Interfaces;
using Orders.Infrastructure.Dtos;

namespace Orders.Infrastructure.Commands.PreOrders
{
    public class SavePreOrder : ICommand
    {
        public string Name { get; set; }
    }
}