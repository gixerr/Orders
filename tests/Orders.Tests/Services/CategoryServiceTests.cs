using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Moq;
using Orders.Core.Domain;
using Orders.Core.Exceptions;
using Orders.Core.Repositories;
using Orders.Infrastructure.Dtos;
using Orders.Infrastructure.Services;
using Xunit;

namespace Orders.Tests.Services
{
    public class CategoryServiceTests
    {
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Fixture _fixture;

        public CategoryServiceTests()
        {
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _mapperMock = new Mock<IMapper>();
            _fixture = new Fixture();
        }

        [Fact]
        public async Task get_all_async_should_invoke_get_all_async_on_repository()
        {
            var categoryService = new CategoryService(_categoryRepositoryMock.Object, _mapperMock.Object);

            await categoryService.GetAllAsync();

            _categoryRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public void get_all_async_should_throw_an_exception_when_repository_is_empty()
        {
            _categoryRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync((IEnumerable<Category>)null);
            var categoryService = new CategoryService(_categoryRepositoryMock.Object, _mapperMock.Object);

            Func<Task> getAllCategories = async () => await categoryService.GetAllAsync();

            var expectedExcepion = getAllCategories.ShouldThrow<OrderException>();
            expectedExcepion.And.ErrorCode.ShouldBeEquivalentTo(ErrorCode.category_not_found);
            expectedExcepion.And.Message.ShouldBeEquivalentTo("No categories available.");
        }

        [Fact]
        public async Task get_async_by_id_should_invoke_gat_async_on_repository()
        {
            var category = _fixture.Create<Category>();
            var categoryDto = _fixture.Create<CategoryDto>();
            _categoryRepositoryMock.Setup(x => x.GetAsync(category.Id)).ReturnsAsync(category);
            _mapperMock.Setup(x => x.Map<CategoryDto>(category)).Returns(categoryDto);
            var categoryService = new CategoryService(_categoryRepositoryMock.Object,
                _mapperMock.Object);

            var expectedCategoryDto = await categoryService.GetAsync(category.Id);

            expectedCategoryDto.Should().NotBeNull();
            _categoryRepositoryMock.Verify(x => x.GetAsync(category.Id), Times.Once);
            _mapperMock.Verify(x => x.Map<CategoryDto>(category));
        }

        [Fact]
        public void get_async_by_id_should_throw_an_exception_when_category_with_given_id_not_exist()
        {
            var category = _fixture.Create<Category>();
            var categoryService = new CategoryService(_categoryRepositoryMock.Object, _mapperMock.Object);

            Func<Task> getCategory = async () => await categoryService.GetAsync(category.Id);

            var expectedExcepion = getCategory.ShouldThrow<OrderException>();
            expectedExcepion.And.ErrorCode.ShouldBeEquivalentTo(ErrorCode.category_not_found);
            expectedExcepion.And.Message.ShouldBeEquivalentTo($"Category with given id '{category.Id}' not found.");
        }

        [Fact]
        public async Task get_async_by_name_should_invoke_get_async_on_repository()
        {
            var category = _fixture.Create<Category>();
            var categoryDto = _fixture.Create<CategoryDto>();
            _categoryRepositoryMock.Setup(x => x.GetAsync(category.Name)).ReturnsAsync(category);
            _mapperMock.Setup(x => x.Map<CategoryDto>(category)).Returns(categoryDto);
            var categoryService = new CategoryService(_categoryRepositoryMock.Object,
                _mapperMock.Object);

            var expectedCategoryDto = await categoryService.GetAsync(category.Name);

            expectedCategoryDto.Should().NotBeNull();
            _categoryRepositoryMock.Verify(x => x.GetAsync(category.Name), Times.Once);
            _mapperMock.Verify(x => x.Map<CategoryDto>(category));
        }

        [Fact]
        public void get_async_by_name_should_throw_an_exception_when_category_with_given_name_not_exist()
        {
            var category = _fixture.Create<Category>();
            var categoryService = new CategoryService(_categoryRepositoryMock.Object, _mapperMock.Object);

            Func<Task> getCategory = async () => await categoryService.GetAsync(category.Name);

            var expectedExcepion = getCategory.ShouldThrow<OrderException>();
            expectedExcepion.And.ErrorCode.ShouldBeEquivalentTo(ErrorCode.category_not_found);
            expectedExcepion.And.Message.ShouldBeEquivalentTo($"Category with given name '{category.Name}' not found.");
        }

        [Fact]
        public async Task add_async_should_invoke_add_async_on_repository()
        {
            var categoryService = new CategoryService(_categoryRepositoryMock.Object, _mapperMock.Object);
            var category = _fixture.Create<Category>();

            await categoryService.AddAsync(category.Name);

            _categoryRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Category>()), Times.Once);
        }

        [Fact]
        public void adding_exinsting_category_should_thow_an_exception()
        {
            var categoryService = new CategoryService(_categoryRepositoryMock.Object, _mapperMock.Object);
            var category = _fixture.Create<Category>();
            _categoryRepositoryMock.Setup(x => x.GetAsync(category.Name)).ReturnsAsync(category);


            Func<Task> addCategory = async () => await categoryService.AddAsync(category.Name);

            var expectedException = addCategory.ShouldThrow<OrderException>();
            expectedException.And.ErrorCode.ShouldBeEquivalentTo(ErrorCode.category_already_exists);
            expectedException.And.Message.ShouldBeEquivalentTo($"Category with given name '{category.Name}' already exist. Category name must be unique.");
        }

        [Fact]
        public async Task remove_async_should_invoke_remove_async_on_repository()
        {
            var categoryService = new CategoryService(_categoryRepositoryMock.Object, _mapperMock.Object);
            var category = _fixture.Create<Category>();
            _categoryRepositoryMock.Setup((x => x.GetAsync(category.Id))).ReturnsAsync(category);

            await categoryService.RemoveAsync(category.Id);

            _categoryRepositoryMock.Verify(x => x.RemoveAsync(category.Id), Times.Once);
        }

        [Fact]
        public void removeing_nonexisting_category_should_throw_an_exception()
        {
            var categoryService = new CategoryService(_categoryRepositoryMock.Object, _mapperMock.Object);
            var category = _fixture.Create<Category>();

            Func<Task> removeCategory = async () => await categoryService.RemoveAsync(category.Id);

            var expectedException = removeCategory.ShouldThrow<OrderException>();
            expectedException.And.ErrorCode.ShouldBeEquivalentTo(ErrorCode.category_not_found);
            expectedException.And.Message.ShouldBeEquivalentTo($"Category with given id '{category.Id}' not found. Unable to remove nonexiting category.");
        }
    }
}