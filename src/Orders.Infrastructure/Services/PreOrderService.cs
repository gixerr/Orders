using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Orders.Core.Domain;
using Orders.Core.Exceptions;
using Orders.Core.Repositories;
using Orders.Infrastructure.Exceptions;
using Orders.Infrastructure.Services.Interfaces;

namespace Orders.Infrastructure.Services
{
    public class PreOrderService : IPreOrderService
    {
        private readonly IMemoryCache _cache;
        private readonly IOrderService _orderService;
        public PreOrderService(IMemoryCache cache, IOrderService orderService)
        {
            _cache = cache;
            _orderService = orderService;
        }

        public async Task Create(string name)
        {
            await _orderService.FailIfExistAsync(name);
            var order = Get(name);
            if (!(order is null))
            {
                throw new ServiceException(ErrorCode.order_already_exists, $"Order with given name: '{name}' already added.");
            }
            _cache.Set(GetKey(name), new Order(GetKey(name)));
        }

        public Order Get(string name)
            => _cache.Get<Order>(GetKey(name));

        public void Remove(string name)
            => _cache.Remove(GetKey(name));

        private string GetKey(string name)
            => $"{name}:order";
    }
}