using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Moq;
using Orders.Core.Domain;
using Orders.Core.Exceptions;
using Orders.Core.Repositories;
using Orders.Infrastructure.Exceptions;
using Orders.Infrastructure.Services;
using Xunit;

namespace Orders.Tests.Services
{
    public class OrderServiceTests
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly Fixture _fixture;

        public OrderServiceTests()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _fixture = new Fixture();
        }

        [Fact]
        public async Task get_all_async_should_invoke_get_all_async_on_repository()
        {
            var orderService = new OrderService(_orderRepositoryMock.Object);

            await orderService.GetAllAsync();

            _orderRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public void get_all_async_should_throw_an_exception_when_repository_is_empty()
        {
            _orderRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync((IEnumerable<Order>)null);
            var orderService = new OrderService(_orderRepositoryMock.Object);

            Func<Task> getAllOrders = async () => await orderService.GetAllAsync();

            var expectedExcepion = getAllOrders.ShouldThrow<ServiceException>();
            expectedExcepion.And.ErrorCode.ShouldBeEquivalentTo(ErrorCode.order_not_found);
            expectedExcepion.And.Message.ShouldBeEquivalentTo("No orders available.");
        }

        [Fact]
        public async Task get_async_by_id_should_invoke_gat_async_on_repository()
        {
            var order = _fixture.Create<Order>();
            _orderRepositoryMock.Setup(x => x.GetAsync(order.Id)).ReturnsAsync(order);
            var orderService = new OrderService(_orderRepositoryMock.Object);

            var expectedOrderDto = await orderService.GetAsync(order.Id);

            expectedOrderDto.Should().NotBeNull();
            _orderRepositoryMock.Verify(x => x.GetAsync(order.Id), Times.Once);
        }

        [Fact]
        public void get_async_by_id_should_throw_an_exception_when_order_with_given_id_not_exist()
        {
            var order = _fixture.Create<Order>();
            var orderService = new OrderService(_orderRepositoryMock.Object);

            Func<Task> getOrder = async () => await orderService.GetAsync(order.Id);

            var expectedExcepion = getOrder.ShouldThrow<ServiceException>();
            expectedExcepion.And.ErrorCode.ShouldBeEquivalentTo(ErrorCode.order_not_found);
            expectedExcepion.And.Message.ShouldBeEquivalentTo($"Order with given id '{order.Id}' not found.");
        }

        [Fact]
        public async Task get_async_by_name_should_invoke_get_async_on_repository()
        {
            var order = _fixture.Create<Order>();
            _orderRepositoryMock.Setup(x => x.GetAsync(order.Name)).ReturnsAsync(order);
            var orderService = new OrderService(_orderRepositoryMock.Object);

            var expectedOrderDto = await orderService.GetAsync(order.Name);

            expectedOrderDto.Should().NotBeNull();
            _orderRepositoryMock.Verify(x => x.GetAsync(order.Name), Times.Once);
        }

        [Fact]
        public void get_async_by_name_should_throw_an_exception_when_order_with_given_name_not_exist()
        {
            var order = _fixture.Create<Order>();
            var orderService = new OrderService(_orderRepositoryMock.Object);

            Func<Task> getOrder = async () => await orderService.GetAsync(order.Name);

            var expectedExcepion = getOrder.ShouldThrow<ServiceException>();
            expectedExcepion.And.ErrorCode.ShouldBeEquivalentTo(ErrorCode.order_not_found);
            expectedExcepion.And.Message.ShouldBeEquivalentTo($"Order with given name '{order.Name}' not found.");
        }


        [Fact]
        public async Task add_async_should_invoke_add_async_on_repository()
        {
            var orderService = new OrderService(_orderRepositoryMock.Object);
            var order = _fixture.Create<Order>();

            await orderService.AddEmptyAsync(order.Name);

            _orderRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Order>()), Times.Once);
        }

        [Fact]
        public void adding_exinsting_order_should_thow_an_exception()
        {
            var orderService = new OrderService(_orderRepositoryMock.Object);
            var order = _fixture.Create<Order>();
            _orderRepositoryMock.Setup(x => x.GetAsync(order.Name)).ReturnsAsync(order);


            Func<Task> addOrder = async () => await orderService.AddEmptyAsync(order.Name);

            var expectedException = addOrder.ShouldThrow<ServiceException>();
            expectedException.And.ErrorCode.ShouldBeEquivalentTo(ErrorCode.order_already_exists);
            expectedException.And.Message.ShouldBeEquivalentTo($"Order with given name '{order.Name}' already exist. Order name must be unique.");
        }

        [Fact]
        public async Task remove_async_should_invoke_remove_async_on_repository()
        {
            var orderService = new OrderService(_orderRepositoryMock.Object);
            var order = _fixture.Create<Order>();
            _orderRepositoryMock.Setup((x => x.GetAsync(order.Id))).ReturnsAsync(order);

            await orderService.RemoveAsync(order.Id);

            _orderRepositoryMock.Verify(x => x.RemoveAsync(order.Id), Times.Once);
        }

        [Fact]
        public void removing_nonexisting_order_should_throw_an_exception()
        {
            var orderService = new OrderService(_orderRepositoryMock.Object);
            var order = _fixture.Create<Order>();

            Func<Task> removeOrder = async () => await orderService.RemoveAsync(order.Id);

            var expectedException = removeOrder.ShouldThrow<ServiceException>();
            expectedException.And.ErrorCode.ShouldBeEquivalentTo(ErrorCode.order_not_found);
            expectedException.And.Message.ShouldBeEquivalentTo($"Order with given id '{order.Id}' not found. Unable to remove nonexiting order.");
        }
    }
}