using System;
using Orders.Core.Domain;

namespace Orders.Infrastructure.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public Role Role { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}