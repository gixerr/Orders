using System;

namespace Orders.Infrastructure.Commands.Categories
{
    public class RemoveCategory : ICommand
    {
        public Guid Id { get; set; }
    }
}