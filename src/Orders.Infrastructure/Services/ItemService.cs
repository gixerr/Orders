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
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemReopository;
        private readonly ICategoryRepository _categoryRepository;

        public ItemService(IItemRepository itemRepository, ICategoryRepository categoryRepository)
        {
            _itemReopository = itemRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<ItemDto>> GetAllAsync()
            => (await _itemReopository.GetAllOrFailAsync()).Dto();

        public async Task<ItemDto> GetAsync(Guid id)
            => (await _itemReopository.GetOrFailAsync(id)).Dto();

        public async Task<IEnumerable<ItemDto>> GetAsync(string name)
            => (await _itemReopository.GetOrFailAsync(name)).Dto();

        public async Task AddAsync(string itemName, decimal price, string categoryName)
        {
            var category = await _categoryRepository.GetOrFailAsync(categoryName);
            await _itemReopository.AddOrFailAsync(itemName, price, category);
        }

        public async Task RemoveAsync(Guid id)
            => await _itemReopository.RemoveOrFailAsync(id);
    }
}