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
    public class ItemServiceTests
    {
        private readonly Mock<IItemRepository> _itemRepositoryMock;
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Fixture _fixture;

        public ItemServiceTests()
        {
            _itemRepositoryMock = new Mock<IItemRepository>();
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _mapperMock = new Mock<IMapper>();
            _fixture = new Fixture();
        }

        [Fact]
        public async Task get_all_async_should_invoke_get_all_async_on_repository()
        {
            var itemService = new ItemService(_itemRepositoryMock.Object, _categoryRepositoryMock.Object, _mapperMock.Object);

            await itemService.GetAllAsync();

            _itemRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public void get_all_async_should_throw_an_exception_when_repository_is_empty()
        {
            _itemRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync((IEnumerable<Item>) null);
            var itemService = new ItemService(_itemRepositoryMock.Object,_categoryRepositoryMock.Object ,_mapperMock.Object);

            Func<Task> getAllItems = async () => await itemService.GetAllAsync();

            var expectedExcepion = getAllItems.ShouldThrow<OrderException>();
            expectedExcepion.And.ErrorCode.ShouldBeEquivalentTo(ErrorCode.item_not_found);
            expectedExcepion.And.Message.ShouldBeEquivalentTo("No items available.");
        }

        [Fact]
        public async Task get_async_by_id_should_invoke_gat_async_on_repository()
        {
            var item = _fixture.Create<Item>();
            var itemDto = _fixture.Create<ItemDto>();
            _itemRepositoryMock.Setup(x => x.GetAsync(item.Id)).ReturnsAsync(item);
            _mapperMock.Setup(x => x.Map<ItemDto>(item)).Returns(itemDto);
            var itemService = new ItemService(_itemRepositoryMock.Object, _categoryRepositoryMock.Object,
                _mapperMock.Object);

            var expectedItemDto = await itemService.GetAsync(item.Id);

            expectedItemDto.Should().NotBeNull();
            _itemRepositoryMock.Verify(x => x.GetAsync(item.Id), Times.Once);
            _mapperMock.Verify(x => x.Map<ItemDto>(item));
        }

        [Fact]
        public void get_async_by_id_should_throw_an_exception_when_item_with_given_id_not_exist()
        {
            var item = _fixture.Create<Item>();
            var itemService = new ItemService(_itemRepositoryMock.Object, _categoryRepositoryMock.Object, _mapperMock.Object);

            Func<Task> getItem = async () => await itemService.GetAsync(item.Id);

            var expectedExcepion = getItem.ShouldThrow<OrderException>();
            expectedExcepion.And.ErrorCode.ShouldBeEquivalentTo(ErrorCode.item_not_found);
            expectedExcepion.And.Message.ShouldBeEquivalentTo($"Item with given id '{item.Id}' not found.");
        }

        [Fact]
        public async Task get_async_by_name_should_invoke_get_async_on_repository()
        {
            var item = _fixture.Create<Item>();
            var items = _fixture.Create<List<Item>>();
            var itemDtos = _fixture.Create<List<ItemDto>>();
            _itemRepositoryMock.Setup(x => x.GetAsync(item.Name)).ReturnsAsync(items);
            _mapperMock.Setup(x => x.Map<IEnumerable<ItemDto>>(items)).Returns(itemDtos);
            var itemService = new ItemService(_itemRepositoryMock.Object, _categoryRepositoryMock.Object,
                _mapperMock.Object);

            var expectedItemsDto = await itemService.GetAsync(item.Name);

            expectedItemsDto.Should().NotBeNull();
            _itemRepositoryMock.Verify(x => x.GetAsync(item.Name), Times.Once);
            _mapperMock.Verify(x => x.Map<IEnumerable<ItemDto>>(items));
        }

        [Fact]
        public void get_async_by_name_should_throw_an_exception_when_item_with_given_name_not_exist()
        {
            
            var item = _fixture.Create<Item>();
            _itemRepositoryMock.Setup(x => x.GetAsync(item.Name)).ReturnsAsync((IEnumerable<Item>)null);
            var itemService = new ItemService(_itemRepositoryMock.Object, _categoryRepositoryMock.Object, _mapperMock.Object);
            
            Func<Task> getItem = async () => await itemService.GetAsync(item.Name);

            var expectedExcepion = getItem.ShouldThrow<OrderException>();
            expectedExcepion.And.ErrorCode.ShouldBeEquivalentTo(ErrorCode.item_not_found);
            expectedExcepion.And.Message.ShouldBeEquivalentTo($"Item with given name '{item.Name}' not found.");
        }

        [Fact]
        public async Task adding_item_with_unique_name_should_invoke_add_async_on_repository()
        {
            var item = _fixture.Create<Item>();
            var category = _fixture.Create<Category>();
            var itemService = new ItemService(_itemRepositoryMock.Object, _categoryRepositoryMock.Object, _mapperMock.Object);
            _itemRepositoryMock.Setup(x => x.GetAsync(item.Name)).ReturnsAsync((IEnumerable<Item>)null);
            _categoryRepositoryMock.Setup(x => x.GetAsync(category.Name)).ReturnsAsync(category);
            
            await itemService.AddAsync(item.Name, item.Price, category.Name);

            _itemRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Item>()), Times.Once);
        }

        [Fact]
        public async Task adding_item_with_existing_name_but_with_different_category_should_invoke_add_async_on_repository()
        {
            var item = _fixture.Create<Item>();
            var category = _fixture.Create<Category>();
            var items = _fixture.Create<List<Item>>();
            var itemService = new ItemService(_itemRepositoryMock.Object, _categoryRepositoryMock.Object, _mapperMock.Object);
            _itemRepositoryMock.Setup(x => x.GetAsync(item.Name)).ReturnsAsync(items);
            _categoryRepositoryMock.Setup(x => x.GetAsync(category.Name)).ReturnsAsync(category);

            await itemService.AddAsync(item.Name, item.Price, category.Name);

            _itemRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Item>()), Times.Once);
        }

        [Fact]
        public void adding_exinsting_item_should_thow_an_exception()
        {
            var items = _fixture.Create<List<Item>>();
            var existingItem = items[0];
            var existingCategory = items[0].Category;

            var itemService = new ItemService(_itemRepositoryMock.Object, _categoryRepositoryMock.Object, _mapperMock.Object);
            _itemRepositoryMock.Setup(x => x.GetAsync(existingItem.Name)).ReturnsAsync(items);
            _categoryRepositoryMock.Setup(x => x.GetAsync(existingCategory.Name)).ReturnsAsync(existingCategory);
           
            Func<Task> addExistingItem = async () => await itemService.AddAsync(existingItem.Name, existingItem.Price, existingCategory.Name);

            var expectedException = addExistingItem.ShouldThrow<OrderException>();
            expectedException.And.ErrorCode.ShouldBeEquivalentTo(ErrorCode.item_already_exists);
            expectedException.And.Message.ShouldBeEquivalentTo($"Item with given name '{existingItem.Name}' and category '{existingCategory.Name}' already exists");
        }

        [Fact]
        public async Task remove_async_should_invoke_remove_async_on_repository()
        {
            var itemService = new ItemService(_itemRepositoryMock.Object, _categoryRepositoryMock.Object, _mapperMock.Object);
            var item = _fixture.Create<Item>();
            _itemRepositoryMock.Setup((x => x.GetAsync(item.Id))).ReturnsAsync(item);

            await itemService.RemoveAsync(item.Id);

            _itemRepositoryMock.Verify(x => x.RemoveAsync(item.Id), Times.Once);
        }

        [Fact]
        public void removing_nonexisting_order_should_throw_an_exception()
        {
            var itemService = new ItemService(_itemRepositoryMock.Object, _categoryRepositoryMock.Object, _mapperMock.Object);
            var item = _fixture.Create<Item>();

            Func<Task> removeItem = async () => await itemService.RemoveAsync(item.Id);

            var expectedException = removeItem.ShouldThrow<OrderException>();
            expectedException.And.ErrorCode.ShouldBeEquivalentTo(ErrorCode.item_not_found);
            expectedException.And.Message.ShouldBeEquivalentTo($"Item with given id '{item.Id}' not found. Unable to remove nonexiting item.");
        }

    }
}