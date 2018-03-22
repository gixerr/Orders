using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orders.Core.Repositories;
using Orders.Infrastructure.Dtos;
using Orders.Infrastructure.Repositories.Extensions;
using Orders.Infrastructure.Services.Extensions;
using Orders.Infrastructure.Services.Interfaces;

namespace Orders.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        
        public async Task<IEnumerable<OrderDto>> GetAllAsync()
            => (await _orderRepository.GetAllOrFailAsync()).Dto();

        public async Task<OrderDto> GetAsync(Guid id)
            => (await _orderRepository.GetOrFailAsync(id)).Dto();

        public async Task<OrderDto> GetAsync(string name)
            => (await _orderRepository.GetOrFailAsync(name)).Dto();

        public async Task<IEnumerable<OrderDto>> GetAsync(StatusDto status)
            => (await _orderRepository.GetOrFailAsync(status)).Dto();
        
        public async Task FailIfExistAsync(string name)
            => await _orderRepository.FailIfExistAsync(name);

        public async Task AddAsync(string name)
            => await _orderRepository.AddOrFailAsync(name); 

        public async Task RemoveAsync(Guid id)
            => await _orderRepository.RemoveOrFailAsync(id);
    }
}