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
    public class OrderServiceTests
    {
        [Fact]
        public async Task get_all_async_should_invoke_get_all_async_on_repository()
        {
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var mapperMock = new Mock<IMapper>();

            var orderService = new OrderService(orderRepositoryMock.Object, mapperMock.Object);
            await orderService.GetAllAsync();

            orderRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task get_async_by_name_should_return_existing_order_with_id_from_in_memory_repository()
        {
            var orderRepository = new InMemoryOrderRepository();
            var mapper = AutoMapperConfig.GetMapper();
            var orderService = new OrderService(orderRepository, mapper);

            var orderDto = await orderService.GetAsync("Order-1");

            orderDto.Id.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public async Task get_async_by_name_should_return_existing_order_with_given_name_from_in_memory_repository()
        {
            var orderRepository = new InMemoryOrderRepository();
            var mapper = AutoMapperConfig.GetMapper();
            var orderService = new OrderService(orderRepository, mapper);

            var orderDto = await orderService.GetAsync("Order-1");

            orderDto.Name.Should().BeEquivalentTo("Order-1");
        }

        [Fact]
        public async Task get_async_by_name_should_throw_exception_when_order_with_given_name_not_exist()
        {
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var mapperMock = new Mock<IMapper>();
            var orderService = new OrderService(orderRepositoryMock.Object, mapperMock.Object);

            try
            {
                var orderDto = await orderService.GetAsync("FakeOrder");
            }
            catch (OrderException oe)
            {
                oe.ErrorCode.Should().BeEquivalentTo(ErrorCode.order_not_found);
                oe.Message.Should().BeEquivalentTo($"Order with given name 'FakeOrder' not found.");
            }
        }

        [Fact]
        public async Task get_async_by_id_should_return_existing_order_with_given_id_from_in_memory_repository()
        {
            var orderRepository = new InMemoryOrderRepository();
            var mapper = AutoMapperConfig.GetMapper();
            var orderService = new OrderService(orderRepository, mapper);

            var orderDto = await orderService.GetAsync("Order-6");
            var orderDtoById = await orderService.GetAsync(orderDto.Id);

            orderDtoById.Id.Should().Be(orderDto.Id);
        }

        [Fact]
        public async Task get_async_by_id_should_throw_exception_when_order_with_given_id_not_exist()
        {
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var mapperMock = new Mock<IMapper>();
            var orderService = new OrderService(orderRepositoryMock.Object, mapperMock.Object);

            try
            {
                var orderDto = await orderService.GetAsync(Guid.Empty);
            }
            catch (OrderException oe)
            {
                oe.ErrorCode.Should().BeEquivalentTo(ErrorCode.order_not_found);
                oe.Message.Should().BeEquivalentTo($"Order with given id '{Guid.Empty}' not found.");
            }
        }

        [Fact]
        public async Task add_async_should_invoke_add_async_on_repository()
        {
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var mapperMock = new Mock<IMapper>();
            var orderService = new OrderService(orderRepositoryMock.Object, mapperMock.Object);
            var order = new Order("Order1");

            await orderService.AddAsync(order.Name);

            orderRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Order>()), Times.Once);
        }

        [Fact]

        public async Task adding_exinsting_order_to_in_memory_repository_should_thow_exception()
        {
            var orderRepository = new InMemoryOrderRepository();
            var mapper = AutoMapperConfig.GetMapper();
            var orderService = new OrderService(orderRepository, mapper);
            var order = new Order("Order-7");

            try
            {
                await orderRepository.AddAsync(order);
            }
            catch (OrderException oe)
            {
                oe.ErrorCode.Should().BeEquivalentTo(ErrorCode.order_already_exists);
                oe.Message.Should().BeEquivalentTo($"Order with given name '{order.Name}' already exist. Order name must be unique.");
            }
        }

        [Fact]
        public async Task trying_to_remove_nonexisting_order_should_throw_exception()
        {
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var mapperMock = new Mock<IMapper>();
            var orderService = new OrderService(orderRepositoryMock.Object, mapperMock.Object);
            
            try
            {
                await orderService.RemoveAsync(Guid.Empty);
            }
            catch (OrderException oe)
            {
                oe.ErrorCode.Should().BeEquivalentTo(ErrorCode.order_not_found);
                oe.Message.Should().BeEquivalentTo($"Order with given id '{Guid.Empty}' not found. Unable to remove nonexiting order.");
            }
        }

        [Fact]
        public async Task get_async_on_removed_order_should_throw_exception()
        {
            var orderRepository = new InMemoryOrderRepository();
            var mapper = AutoMapperConfig.GetMapper();
            var orderService = new OrderService(orderRepository, mapper);
            
            var orderDto = await orderService.GetAsync("Order-5");
            await orderService.RemoveAsync(orderDto.Id);
            try
            {
                await orderService.GetAsync("Order-5");
            }
            catch (OrderException oe)
            {
                oe.ErrorCode.Should().BeEquivalentTo(ErrorCode.order_not_found);
                oe.Message.Should().BeEquivalentTo($"Order with given name 'Order-5' not found.");
            }
        }
    }
}