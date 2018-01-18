using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orders.Core.Domain;

namespace Orders.Core.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetAsync(Guid id);
        Task<Category> GetAsync(string name);
        Task AddAsync(string name);
        Task UpdateAsync(Guid id);
        Task RemoveAsync(Guid id);
    }
}