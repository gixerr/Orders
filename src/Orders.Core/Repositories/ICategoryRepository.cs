using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orders.Core.Domain;

namespace Orders.Core.Repositories
{
    public interface ICategoryRepository : IRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetAsync(Guid id);
        Task<Category> GetAsync(string name);
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task RemoveAsync(Guid id);
    }
}