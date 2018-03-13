using System;
using Orders.Core.Domain;
using Orders.Infrastructure.Commands.Interfaces;

namespace Orders.Infrastructure.Commands.Users
{
    public class Register : ICommand
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}