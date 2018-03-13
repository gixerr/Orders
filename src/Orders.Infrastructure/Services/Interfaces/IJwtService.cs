using System;
using Orders.Core.Domain;
using Orders.Infrastructure.Dtos;

namespace Orders.Infrastructure.Services.Interfaces
{
    public interface IJwtService : IService
    {
         JsonWebTokenDto CreateToken(Guid id, Role role);
    }
}