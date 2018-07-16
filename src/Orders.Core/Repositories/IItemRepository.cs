using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orders.Core.Domain;

namespace Orders.Core.Repositories
{
    public interface IItemRepository : IRepository
    {
        Task<IEnumerable<Item>> GetAllAsync();
        Task<Item> GetAsync(Guid id);
        Task<IEnumerable<Item>> GetAsync(string name);
        Task AddAsync(Item item);
        Task UpdateAsync(Item item);
        Task RemoveAsync(Guid id);
    }
}