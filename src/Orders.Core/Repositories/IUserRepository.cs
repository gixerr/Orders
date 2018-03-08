using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orders.Core.Domain;

namespace Orders.Core.Repositories
{
    public interface IUserRepository : IRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetAsync(Guid id);
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByNameAsync(string name);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task RemoveAsync(User user);

    }
}