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

        public Task<IEnumerable<Category>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetAsync(string name)
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

        private static IEnumerable<Category> InitializeCategories()
        {
            for (int i = 0; i < 10; i++)
            {
                yield return new Category($"Category{i+1}");
            }
        }

    }
}