using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orders.Core.Domain;
using Orders.Infrastructure.Dtos;

namespace Orders.Infrastructure.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllAsync();
        Task<OrderDto> GetAsync(Guid id);
        Task<OrderDto> GetAsync(string name);
        Task<IEnumerable<OrderDto>> GetAsync(StatusDto status);
        Task AddAsync(string name);
        Task RemoveAsync(Guid id);
    }
}