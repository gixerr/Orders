using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orders.Core.Domain;

namespace Orders.Core.Repositories
{
    public interface IItemRepository
    {
        Task<IEnumerable<Item>> GetAllAsync();
        Task<Item> GetAsync(Guid id);
        Task<Item> GetAsync(string name);
        Task AddAsync(string name);
        Task UpdateAsync(Guid id);
        Task RemoveAsync(Guid id);
    }
}