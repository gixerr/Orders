using System;
using System.Threading.Tasks;

namespace Orders.Infrastructure.Services.Interfaces
{
    public interface IOrderItemService : IService
    {
        Task AddAsync(Guid orderId, Guid itemId);
        Task RemoveAsync(Guid orderId, Guid itemId);
        Task ClearAsync(Guid orderId);
    }
}