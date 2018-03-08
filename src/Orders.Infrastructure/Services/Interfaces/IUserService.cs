using System;
using System.Threading.Tasks;
using Orders.Core.Domain;
using Orders.Infrastructure.Dtos;

namespace Orders.Infrastructure.Services.Interfaces
{
    public interface IUserService : IService
    {
        Task RegisterAsync(Guid id, string name, string email, string password, Role role);
        Task<JsonWebTokenDto> LoginAsync(string email, string password);
    }
}