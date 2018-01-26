using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Orders.Core.Domain;
using Orders.Core.Repositories;

namespace Orders.Infrastructure.Repositories
{
    public class InMemoryItemRepository : IItemRepository
    {
        private static List<Item> _items = InitializeItems().ToList();

        public async Task<IEnumerable<Item>> GetAllAsync()
            => await Task.FromResult(_items);

        public async Task<Item> GetAsync(Guid id)
            => await Task.FromResult(_items.SingleOrDefault(i => i.Id == id));

        public async Task<IEnumerable<Item>> GetAsync(string name)
            => await Task.FromResult(_items.Where(i => i.Name.ToLowerInvariant() == name.ToLowerInvariant()));

        public async Task AddAsync(Item item)
        {
            _items.Add(item);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Guid id)
        {
            await Task.CompletedTask;
        }

        public async Task RemoveAsync(Guid id)
        {
            _items.Remove(await GetAsync(id));
        }

        private static IEnumerable<Item> InitializeItems()
        {
            for (int i = 0; i < 10; i++)
            {
                if (i < 5)
                {
                    yield return new Item($"Item-{i + 1}", InMemoryCategoryRepository.Categories[i]);
                    continue;
                }
                yield return new Item($"Item-{i - 4}", InMemoryCategoryRepository.Categories[i]);
            }
        }

    }
}