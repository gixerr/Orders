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

        public Task<IEnumerable<Item>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Item> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Item> GetAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        private static IEnumerable<Item> InitializeItems()
        {
            for (int i = 0; i < 10; i++)
            {
                if (i < 5)
                {
                    yield return new Item($"Item-{i + 1}", InMemoryCategoryRepository.Categories[i]);
                }
                yield return new Item($"Item-{i - 4}", InMemoryCategoryRepository.Categories[i]);
            }
        }

    }
}