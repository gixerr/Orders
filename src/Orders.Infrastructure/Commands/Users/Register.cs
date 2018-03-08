using System;
using Orders.Core.Domain;
using Orders.Infrastructure.Commands.Interfaces;

namespace Orders.Infrastructure.Commands.Users
{
    public class Register : ICommand
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public string Salt { get; protected set; }
        public Role Role { get; protected set; }
    }
}