using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Orders.Core.Domain;
using Orders.Core.Repositories;

namespace Orders.Infrastructure.Repositories
{
    public class InMemoryOrderRepository : IOrderRepository
    {
        private static List<Order> _orders = InitializeOrders().ToList();

        public async Task<IEnumerable<Order>> GetAllAsync()
            => await Task.FromResult(_orders);

        public Task<Order> GetAsync(Guid id)
            => Task.FromResult(_orders.SingleOrDefault(o => o.Id == id));

        public Task<Order> GetAsync(string name)
            => Task.FromResult(_orders.SingleOrDefault(o => o.Name.ToLowerInvariant() == name.ToLowerInvariant()));

        public Task<IEnumerable<Order>> GetAsync(Status status)
            => Task.FromResult(_orders.Where(o => o.Status == status));

        public async Task AddAsync(Order order)
        {
            _orders.Add(order);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Guid id)
        {
            await Task.CompletedTask;
        }

        public async Task RemoveAsync(Guid id)
        {
            _orders.Remove(await GetAsync(id));
        }

        private static IEnumerable<Order> InitializeOrders()
        {
            for (int i = 0; i < 10; i++)
            {
                yield return Order.Empty($"Order-{i+1}");
            }
        }
    }
}