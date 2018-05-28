using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Orders.Core.Domain;
using Orders.Core.Exceptions;
using Orders.Core.Repositories;
using Orders.Infrastructure.Dtos;
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

        public async Task CreateAsync(string name)
        {
            await _orderService.FailIfExistAsync(name);
            var preOrder = Get(name);
            if (!(preOrder is null))
            {
                throw new ServiceException(ErrorCode.order_already_exists, $"Order with given name: '{name}' already exists.");
            }
            _cache.Set(GetKey(name), new PreOrder(name));
        }

        public PreOrder Get(string name)
            => _cache.Get<PreOrder>(GetKey(name));

        public void Update(PreOrder preOrder)
            => _cache.Set(GetKey(preOrder.Name), preOrder);

        public void Remove(string name)
            => _cache.Remove(GetKey(name));

        public async Task SaveAsync(string name)
        {
            await _orderService.AddAsync(Get(name));
            Remove(name);
        }
            
        private string GetKey(string name)
            => $"{name}:order";
    }
}