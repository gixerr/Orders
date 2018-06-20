using System;
using Orders.Infrastructure.Commands.Interfaces;

namespace Orders.Infrastructure.Commands.Users
{
    public class AssignOrder : ICommand
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
    }
}