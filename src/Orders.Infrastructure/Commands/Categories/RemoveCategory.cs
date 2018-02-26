using System;
using Orders.Infrastructure.Commands.Interfaces;

namespace Orders.Infrastructure.Commands.Categories
{
    public class RemoveCategory : ICommand
    {
        public Guid Id { get; set; }
    }
}