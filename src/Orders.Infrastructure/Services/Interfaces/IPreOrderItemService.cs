using System;
using System.Threading.Tasks;

namespace Orders.Infrastructure.Services.Interfaces
{
    public interface IPreOrderItemService : IService
    {
         Task AddAsync(Guid itemId, string orderName);
    }
}