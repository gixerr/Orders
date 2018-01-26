using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Orders.Core.Domain;
using Orders.Core.Repositories;

namespace Orders.Infrastructure.Repositories
{
    public class InMemoryCategoryRepository : ICategoryRepository
    {
        private static List<Category> _categories = InitializeCategories().ToList();
        public static List<Category> Categories { get; } = _categories;

        public async Task<IEnumerable<Category>> GetAllAsync() 
            => await Task.FromResult(_categories);

        public async Task<Category> GetAsync(Guid id) 
            => await Task.FromResult(_categories.SingleOrDefault(c => c.Id == id));

        public async Task<Category> GetAsync(string name)
            => await Task.FromResult(_categories.SingleOrDefault(c => c.Name.ToLowerInvariant() == name.ToLowerInvariant()));

        public async Task AddAsync(Category category)
        {
            _categories.Add(category);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Guid id)
        {
            await Task.CompletedTask;
        }

        public async Task RemoveAsync(Guid id)
        {
            _categories.Remove(await GetAsync(id));
        }

        private static IEnumerable<Category> InitializeCategories()
        {
            for (int i = 0; i < 10; i++)
            {
                yield return new Category($"Category-{i+1}");
            }
        }

    }
}