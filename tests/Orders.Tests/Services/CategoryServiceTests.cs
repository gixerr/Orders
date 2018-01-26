using System;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Moq;
using Orders.Core.Domain;
using Orders.Core.Exceptions;
using Orders.Core.Repositories;
using Orders.Infrastructure.Mappings;
using Orders.Infrastructure.Repositories;
using Orders.Infrastructure.Services;
using Xunit;

namespace Orders.Tests.Services
{
    public class CategoryServiceTests
    {
        [Fact]
        public async Task get_all_async_should_invoke_get_all_async_on_repository()
        {
            var categoryRepositoryMock = new Mock<ICategoryRepository>();
            var mapperMock = new Mock<IMapper>();

            var categoryService = new CategoryService(categoryRepositoryMock.Object, mapperMock.Object);
            await categoryService.GetAllAsync();

            categoryRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task get_async_by_name_should_return_existing_category_with_id_from_in_memory_repository()
        {
            var categoryRepository = new InMemoryCategoryRepository();
            var mapper = AutoMapperConfig.GetMapper();
            var categoryService = new CategoryService(categoryRepository, mapper);

            var CategoryDto = await categoryService.GetAsync("Category-1");

            CategoryDto.Id.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public async Task get_async_by_name_should_return_existing_category_with_given_name_from_in_memory_repository()
        {
            var categoryRepository = new InMemoryCategoryRepository();
            var mapper = AutoMapperConfig.GetMapper();
            var categoryService = new CategoryService(categoryRepository, mapper);

            var categoryDto = await categoryService.GetAsync("Category-2");

            categoryDto.Name.Should().BeEquivalentTo("Category-2");
        }

        [Fact]
        public async Task get_async_by_name_should_throw_exception_when_category_with_given_name_not_exist()
        {
            var categoryRepositoryMock = new Mock<ICategoryRepository>();
            var mapperMock = new Mock<IMapper>();
            var categoryService = new CategoryService(categoryRepositoryMock.Object, mapperMock.Object);

            try
            {
                var categoryDto = await categoryService.GetAsync("FakeCategory");
            }
            catch (OrderException oe)
            {
                oe.ErrorCode.Should().BeEquivalentTo(ErrorCode.category_not_found);
                oe.Message.Should().BeEquivalentTo($"Category with given name 'FakeCategory' not found.");
            }
        }

        [Fact]
        public async Task get_async_by_id_should_return_existing_category_with_given_id_from_in_memory_repository()
        {
            var categoryRepository = new InMemoryCategoryRepository();
            var mapper = AutoMapperConfig.GetMapper();
            var categoryService = new CategoryService(categoryRepository, mapper);

            var categoryDto = await categoryService.GetAsync("Category-6");
            var categoryDtoById = await categoryService.GetAsync(categoryDto.Id);

            categoryDtoById.Id.Should().Be(categoryDto.Id);
        }

        [Fact]
        public async Task get_async_by_id_should_throw_exception_when_category_with_given_id_not_exist()
        {
            var categoryRepositoryMock = new Mock<ICategoryRepository>();
            var mapperMock = new Mock<IMapper>();
            var categoryService = new CategoryService(categoryRepositoryMock.Object, mapperMock.Object);

            try
            {
                var categoryDto = await categoryService.GetAsync(Guid.Empty);
            }
            catch (OrderException oe)
            {
                oe.ErrorCode.Should().BeEquivalentTo(ErrorCode.category_not_found);
                oe.Message.Should().BeEquivalentTo($"Category with given id '{Guid.Empty}' not found.");
            }
        }

        [Fact]
        public async Task add_async_should_invoke_add_async_on_repository()
        {
            var categoryRepositoryMock = new Mock<ICategoryRepository>();
            var mapperMock = new Mock<IMapper>();
            var categoryService = new CategoryService(categoryRepositoryMock.Object, mapperMock.Object);
            var category = new Category("NewCategory");

            await categoryService.AddAsync(category.Name);

            categoryRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Category>()), Times.Once);
        }

        [Fact]

        public async Task adding_exinsting_category_to_in_memory_repository_should_thow_exception()
        {
            var categoryRepository = new InMemoryCategoryRepository();
            var mapper = AutoMapperConfig.GetMapper();
            var categoryService = new CategoryService(categoryRepository, mapper);
            var category = new Category("Category-4");

            try
            {
                await categoryService.AddAsync(category.Name);
            }
            catch (OrderException oe)
            {
                oe.ErrorCode.Should().BeEquivalentTo(ErrorCode.category_already_exists);
                oe.Message.Should().BeEquivalentTo($"Category with given name '{category.Name}' already exist. Category name must be unique.");
            }
        }

        [Fact]
        public async Task trying_to_remove_nonexisting_category_should_throw_exception()
        {
            var categoryRepositoryMock = new Mock<ICategoryRepository>();
            var mapperMock = new Mock<IMapper>();
            var categoryService = new CategoryService(categoryRepositoryMock.Object, mapperMock.Object);
            
            try
            {
                await categoryService.RemoveAsync(Guid.Empty);
            }
            catch (OrderException oe)
            {
                oe.ErrorCode.Should().BeEquivalentTo(ErrorCode.category_not_found);
                oe.Message.Should().BeEquivalentTo($"Category with given id '{Guid.Empty}' not found. Unable to remove nonexiting category.");
            }
        }

        [Fact]
        public async Task get_async_on_removed_category_should_throw_exception()
        {
            var categoryRepository = new InMemoryCategoryRepository();
            var mapper = AutoMapperConfig.GetMapper();
            var categoryService = new CategoryService(categoryRepository, mapper);
            
            var categoryDto = await categoryService.GetAsync("Category-5");
            await categoryService.RemoveAsync(categoryDto.Id);
            try
            {
                await categoryService.GetAsync("Category-5");
            }
            catch (OrderException oe)
            {
                oe.ErrorCode.Should().BeEquivalentTo(ErrorCode.category_not_found);
                oe.Message.Should().BeEquivalentTo($"Category with given name 'Category-5' not found.");
            }
        }
    }
}