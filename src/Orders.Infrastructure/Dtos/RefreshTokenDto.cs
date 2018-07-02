using System;
using Orders.Core.Domain;

namespace Orders.Infrastructure.Dtos
{
    public class RefreshTokenDto
    {
        public Guid UserId { get; set; }
        public Role UserRole { get; set; }
        public string Token { get; set; }
        public bool Revoked { get; set; }
    }
}