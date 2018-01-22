using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orders.Infrastructure.Dtos;

namespace Orders.Infrastructure.Services.Interfaces
{
    public interface ICategoryService
    {
         Task<IEnumerable<CategoryDto>> GetAllAsync();
         Task<CategoryDto> GetAsync(Guid id);
         Task<CategoryDto> GetAsync(string name);
         Task AddAsync(string name);
         Task RemoveAsync(Guid id);
    }
}