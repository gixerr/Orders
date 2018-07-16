using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Orders.Core.Domain;
using Orders.Core.Exceptions;
using Orders.Core.Repositories;
using Orders.Infrastructure.Exceptions;

namespace Orders.Infrastructure.Repositories.Extensions
{
    public static class ItemRepositoryExtensions
    {
        //TODO: Refactor to fluent
        public static async Task<IEnumerable<Item>> GetAllOrFailAsync(this IItemRepository itemRepository)
        {
            var items = await itemRepository.GetAllAsync();
            if (items is null)
            {
                throw new ServiceException(ErrorCode.item_not_found, "No items available.");
            }

            return items;
        }
        public static async Task<Item> GetOrFailAsync(this IItemRepository itemRepository, Guid id)
        {
            var item = await itemRepository.GetAsync(id);
            if (item is null)
            {
                throw new ServiceException(ErrorCode.item_not_found, $"Item with given id '{id}' not found.");
            }

            return item;
        }

        public static async Task<IEnumerable<Item>> GetOrFailAsync(this IItemRepository itemRepository, string name)
        {
            var items = await itemRepository.GetAsync(name);
            if (items.AreEmpty())
            {
                throw new ServiceException(ErrorCode.item_not_found, $"Item with given name '{name}' not found.");
            }

            return items;
        }

        public static async Task AddOrFailAsync(this IItemRepository itemRepository, string name, decimal price, Category category)
        {
            var items = await itemRepository.GetAsync(name);          
            if (!(items is null) && items.Any(i => i.Category.Name.ToLowerInvariant() == category.Name.ToLowerInvariant()))
            {
               throw new ServiceException(ErrorCode.item_already_exists, $"Item with given name '{name}' and category '{category.Name}' already exists");
            }
            await itemRepository.AddAsync(new Item(name, price, category));
        }

        public static async Task RemoveOrFailAsync(this IItemRepository itemRepository, Guid id)
        {
            var item = await itemRepository.GetAsync(id);
            if (item is null)
            {
                throw new ServiceException(ErrorCode.item_not_found, $"Item with given id '{id}' not found. Unable to remove nonexiting item.");
            }
            await itemRepository.RemoveAsync(id);
        }

        private static bool AreEmpty(this IEnumerable<Item> items)
            => items is null || !items.Any();
    }
}