using System;
using System.Collections.Generic;
using Orders.Core.Domain;

namespace Orders.Infrastructure.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; protected set; }
        public Role Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Order> Orders { get; set; }
    }
}