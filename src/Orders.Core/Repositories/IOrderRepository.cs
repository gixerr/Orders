using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orders.Core.Domain;

namespace Orders.Core.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order> GetAsync(Guid id);
        Task<Order> GetAsync(string name);
        Task AddAsync(Order order);
        Task UpdateAsync(Guid id);
        Task RemoveAsync(Guid id);
    }
}