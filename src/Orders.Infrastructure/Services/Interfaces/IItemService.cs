using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orders.Infrastructure.Dtos;

namespace Orders.Infrastructure.Services.Interfaces
{
    public interface IItemService
    {
         Task<IEnumerable<ItemDto>> GetAllAsync();
         Task<ItemDto> GetAsync(Guid id);
         Task<IEnumerable<ItemDto>> GetAsync(string name);
         Task AddAsync(string itemName, string categoryName);
         Task RemoveAsync(Guid id);
    }
}