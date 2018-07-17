using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Orders.Core.Domain;
using Orders.Core.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Orders.Infrastructure.Repositories
{
    public class InMemoryUserRepository : IUserRepository
    {
        private static List<User> _users = new List<User>();

        public async Task<IEnumerable<User>> GetAllAsync()
            => await Task.FromResult(_users);

        public async Task<User> GetAsync(Guid id)
            => await Task.FromResult(_users.SingleOrDefault(u => u.Id == id));

        public async Task<User> GetByEmailAsync(string email)
             => await Task.FromResult(_users.SingleOrDefault(u => u.Email.ToLowerInvariant() == email.ToLowerInvariant()));

        public async Task<User> GetByNameAsync(string name)
            => await Task.FromResult(_users.SingleOrDefault(u => u.Name.ToLowerInvariant() == name.ToLowerInvariant()));

        public async Task AddAsync(User user)
        {
            _users.Add(user);
            await Task.CompletedTask;
        }
        
        public async Task UpdateAsync(User user)
        {
            await Task.CompletedTask;
        }

        public async Task RemoveAsync(User user)
        {
            _users.Remove(user);
            await Task.CompletedTask;
        }
    }
}