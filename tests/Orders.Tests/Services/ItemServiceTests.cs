using System;
using System.Linq;
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
    public class ItemServiceTests
    {
        [Fact]
        public async Task get_all_async_should_invoke_get_all_async_on_repository()
        {
            var itemRepositoryMock = new Mock<IItemRepository>();
            var categoryRepositoryMock = new Mock<ICategoryRepository>();
            var mapperMock = new Mock<IMapper>();

            var itemService = new ItemService(itemRepositoryMock.Object, categoryRepositoryMock.Object, mapperMock.Object);
            await itemService.GetAllAsync();

            itemRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task get_async_by_name_should_return_existing_items_with_id_from_in_memory_repository()
        {
            var itemRepository = new InMemoryItemRepository();
            var categoryRepository = new InMemoryCategoryRepository();
            var mapper = AutoMapperConfig.GetMapper();
            var itemService = new ItemService(itemRepository, categoryRepository, mapper);

            var itemDtoList = await itemService.GetAsync("Item-1");

            itemDtoList.All(x => x.Id != Guid.Empty).Should().BeTrue();
        }

        [Fact]
        public async Task get_async_by_name_should_return_existing_items_with_given_name_from_in_memory_repository()
        {
            var itemRepository = new InMemoryItemRepository();
            var categoryRepository = new InMemoryCategoryRepository();
            var mapper = AutoMapperConfig.GetMapper();
            var itemService = new ItemService(itemRepository, categoryRepository, mapper);

            var itemDtoList = await itemService.GetAsync("Item-2");

            itemDtoList.All(x => x.Name == "Item-2").Should().BeTrue();
        }

        [Fact]
        public async Task get_async_by_name_should_throw_exception_when_item_with_given_name_not_exist()
        {
            var itemRepositoryMock = new Mock<IItemRepository>();
            var categoryRepositoryMock = new Mock<ICategoryRepository>();
            var mapperMock = new Mock<IMapper>();
            var itemService = new ItemService(itemRepositoryMock.Object, categoryRepositoryMock.Object, mapperMock.Object);

            try
            {
                var orderDto = await itemService.GetAsync("FakeItem");
            }
            catch (OrderException oe)
            {
                oe.ErrorCode.Should().BeEquivalentTo(ErrorCode.item_not_found);
                oe.Message.Should().BeEquivalentTo($"Item with given name 'FakeItem' not found.");
            }
        }

        [Fact]
        public async Task get_async_by_id_should_return_existing_item_with_given_id_from_in_memory_repository()
        {
            var itemRepository = new InMemoryItemRepository();
            var categoryRepository = new InMemoryCategoryRepository();
            var mapper = AutoMapperConfig.GetMapper();
            var itemService = new ItemService(itemRepository, categoryRepository, mapper);

            var itemDtoList = await itemService.GetAsync("Item-3");
            var itemDto = itemDtoList.FirstOrDefault(x => x.Name == "Item-3");
            var itemDtoById = await itemService.GetAsync(itemDto.Id);

            itemDtoById.Id.Should().Be(itemDto.Id);
        }

        [Fact]
        public async Task get_async_by_id_should_throw_exception_when_item_with_given_id_not_exist()
        {
            var itemRepositoryMock = new Mock<IItemRepository>();
            var categoryRepositoryMock = new Mock<ICategoryRepository>();
            var mapperMock = new Mock<IMapper>();
            var orderService = new ItemService(itemRepositoryMock.Object, categoryRepositoryMock.Object, mapperMock.Object);

            try
            {
                var itemDto = await orderService.GetAsync(Guid.Empty);
            }
            catch (OrderException oe)
            {
                oe.ErrorCode.Should().BeEquivalentTo(ErrorCode.item_not_found);
                oe.Message.Should().BeEquivalentTo($"Item with given id '{Guid.Empty}' not found.");
            }
        }

        [Fact]
        public async Task add_async_should_invoke_add_async_on_repository()
        {
            var itemRepositoryMock = new Mock<IItemRepository>();
            var categoryRepository = new InMemoryCategoryRepository();
            var mapperMock = new Mock<IMapper>();
            var itemService = new ItemService(itemRepositoryMock.Object, categoryRepository, mapperMock.Object);
            var item = new Item("NewItem", new Category("Category-4"));

            await itemService.AddAsync(item.Name, item.Category.Name);

            itemRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Item>()), Times.Once);
        }

        [Fact]
        public async Task adding_exinsting_item_to_in_memory_repository_should_thow_exception()
        {
            var itemRepository = new InMemoryItemRepository();
            var categoryRepository = new InMemoryCategoryRepository();
            var mapperMock = new Mock<IMapper>();
            var itemService = new ItemService(itemRepository, categoryRepository, mapperMock.Object);
            var item = new Item("Item-5", new Category("Category-5"));

            try
            {
                await itemService.AddAsync(item.Name, item.Category.Name);
            }
            catch (OrderException oe)
            {
                oe.ErrorCode.Should().BeEquivalentTo(ErrorCode.item_already_exists);
                oe.Message.Should().BeEquivalentTo($"Item with given name '{item.Name}' and category '{item.Category.Name}' already exists");
            }
        }

        [Fact]
        public async Task trying_to_remove_nonexisting_item_should_throw_exception()
        {
            var itemRepositoryMock = new Mock<IItemRepository>();
            var categoryRepositoryMock = new Mock<ICategoryRepository>();
            var mapperMock = new Mock<IMapper>();
            var itemService = new ItemService(itemRepositoryMock.Object, categoryRepositoryMock.Object, mapperMock.Object);

            try
            {
                await itemService.RemoveAsync(Guid.Empty);
            }
            catch (OrderException oe)
            {
                oe.ErrorCode.Should().BeEquivalentTo(ErrorCode.item_not_found);
                oe.Message.Should().BeEquivalentTo($"Item with given id '{Guid.Empty}' not found. Unable to remove nonexiting item.");
            }
        }

        [Fact]
        public async Task get_async_on_removed_item_should_throw_exception()
        {
            var itemRepository = new InMemoryItemRepository();
            var categoryRepositoryMock = new Mock<InMemoryCategoryRepository>();
            var mapper = AutoMapperConfig.GetMapper();
            var itemService = new ItemService(itemRepository, categoryRepositoryMock.Object, mapper);

            var itemDtoList = await itemService.GetAsync("Item-2");
            var itemDto = itemDtoList.FirstOrDefault(x => x.Name == "Item-2");
            await itemService.RemoveAsync(itemDto.Id);

            try
            {
                await itemService.GetAsync("Item-2");
            }
            catch (OrderException oe)
            {
                oe.ErrorCode.Should().BeEquivalentTo(ErrorCode.item_not_found);
                oe.Message.Should().BeEquivalentTo($"Item with given name '{itemDto.Name}' not found.");
            }
        }
    }
}