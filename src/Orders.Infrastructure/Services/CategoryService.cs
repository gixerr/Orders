using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Orders.Core.Repositories;
using Orders.Infrastructure.Dtos;
using Orders.Infrastructure.Repositories.Extensions;
using Orders.Infrastructure.Services.Extensions;
using Orders.Infrastructure.Services.Interfaces;

namespace Orders.Infrastructure.Services
{
    public class CategoryService : Service, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper) : base(mapper)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
            => (await _categoryRepository.GetAllOrFailAsync()).Dto(_mapper);

        public async Task<CategoryDto> GetAsync(Guid id)
            => (await _categoryRepository.GetOrFailAsync(id)).Dto(_mapper);

        public async Task<CategoryDto> GetAsync(string name)
            => (await _categoryRepository.GetOrFailAsync(name)).Dto(_mapper);

        public async Task AddAsync(string name)
            => await _categoryRepository.AddOrFailAsync(name);
        public Task RemoveAsync(Guid id)
            => _categoryRepository.RemoveOrFailAsync(id);
    }
}