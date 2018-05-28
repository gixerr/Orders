using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orders.Core.Domain;
using Orders.Infrastructure.Dtos;

namespace Orders.Infrastructure.Services.Interfaces
{
    public interface IOrderService : IService
    {
        Task<IEnumerable<OrderDto>> GetAllAsync();
        Task<OrderDto> GetAsync(Guid id);
        Task<OrderDto> GetAsync(string name);
        Task FailIfExistAsync(string name);
        Task<IEnumerable<OrderDto>> GetAsync(StatusDto status);
        Task AddAsync(PreOrder preOrder);
        Task AddEmptyAsync(string name);
        Task UpdateAsync(Guid id, string name, Status status);
        Task RemoveAsync(Guid id);
    }
}