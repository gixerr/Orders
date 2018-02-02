using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orders.Core.Domain;
using Orders.Core.Exceptions;
using Orders.Core.Repositories;
using Orders.Infrastructure.Exceptions;

namespace Orders.Infrastructure.Repositories.Extensions
{
    public static class CategoryRepositoryExtensions
    {
        public static async Task<IEnumerable<Category>> GetAllOrFailAsync(this ICategoryRepository categoryRepository)
        {
            var categories = await categoryRepository.GetAllAsync();
            if (categories is null)
            {
                throw new ServiceException(ErrorCode.category_not_found, "No categories available.");
            }

            return categories;
        }
        public static async Task<Category> GetOrFailAsync(this ICategoryRepository categoryRepository, Guid id)
        {
            var category = await categoryRepository.GetAsync(id);
            if (category is null)
            {
                throw new ServiceException(ErrorCode.category_not_found, $"Category with given id '{id}' not found.");
            }

            return category;
        }

        public static async Task<Category> GetOrFailAsync(this ICategoryRepository categoryRepository, string name)
        {
            var category = await categoryRepository.GetAsync(name);
            if (category is null)
            {
                throw new ServiceException(ErrorCode.category_not_found, $"Category with given name '{name}' not found.");
            }

            return category;
        }

        public static async Task AddOrFailAsync(this ICategoryRepository categoryRepository, string name)
        {
            var category = await categoryRepository.GetAsync(name);
            if(!(category is null))
            {
                throw new ServiceException(ErrorCode.category_already_exists, $"Category with given name '{name}' already exist. Category name must be unique.");
            }
            category = new Category(name);
            await categoryRepository.AddAsync(category);
        }

        public static async Task RemoveOrFailAsync(this ICategoryRepository categoryRepository, Guid id)
        {
            var category = await categoryRepository.GetAsync(id);
            if (category is null)
            {
                throw new ServiceException(ErrorCode.category_not_found, $"Category with given id '{id}' not found. Unable to remove nonexiting category.");
            }
            await categoryRepository.RemoveAsync(id);
        }
    }
}