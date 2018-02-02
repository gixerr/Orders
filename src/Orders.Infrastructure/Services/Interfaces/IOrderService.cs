using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orders.Infrastructure.Dtos;

namespace Orders.Infrastructure.Services.Interfaces
{
    public interface IOrderService
    {
         Task<IEnumerable<OrderDto>> GetAllAsync();
         Task<OrderDto> GetAsync(Guid id);
         Task<OrderDto> GetAsync(string name);
         Task AddAsync(string name);
         Task RemoveAsync(Guid id);
    }
}