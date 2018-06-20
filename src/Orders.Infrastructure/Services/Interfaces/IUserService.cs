using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orders.Core.Domain;
using Orders.Infrastructure.Dtos;

namespace Orders.Infrastructure.Services.Interfaces
{
    public interface IUserService : IService
    {
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task RegisterAsync(string name, string email, string password, Role role);
        Task<TokenDto> LoginAsync(string email, string password);
        Task AssignOrderToUserAsync(Guid orderId, Guid userId);
    }
}