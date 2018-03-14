using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orders.Infrastructure.Dtos;

namespace Orders.Infrastructure.Services.Interfaces
{
    public interface IOrderService : IService
    {
        Task<IEnumerable<OrderDto>> GetAllAsync();
        Task<OrderDto> GetAsync(Guid id);
        Task<OrderDto> GetAsync(string name);
        Task<IEnumerable<OrderDto>> GetAsync(StatusDto status);
        Task AddAsync(Guid id, string name);
        Task RemoveAsync(Guid id);
    }
}