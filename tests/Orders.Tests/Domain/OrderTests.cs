using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using Orders.Core.Domain;
using Orders.Core.Exceptions;
using Xunit;

namespace Orders.Tests.Domain
{
    public class OrderTests
    {
        private readonly Fixture _fixture;

        public OrderTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void providing_empty_name_should_throw_an_exception()
        {
            var name = string.Empty;

            Action createNewOrder = () => Order.Empty(name);

            var expectedException = createNewOrder.ShouldThrow<OrdersException>();
            expectedException.And.ErrorCode.ShouldBeEquivalentTo(ErrorCode.invalid_name);
            expectedException.And.Message.ShouldBeEquivalentTo("Name cannot be empty.");
        }

        [Fact]
        public void passing_null_as_name_should_throw_an_exception()
        {
            string name = null;

            Action createNewOrder = () => Order.Empty(name);

            var expectedException = createNewOrder.ShouldThrow<OrdersException>();
            expectedException.And.ErrorCode.ShouldBeEquivalentTo(ErrorCode.invalid_name);
            expectedException.And.Message.ShouldBeEquivalentTo("Name cannot be empty.");
        }

        [Fact]
        public void new_order_id_should_not_be_empty()
        {
            var name = _fixture.Create<string>();
            var preOrder = _fixture.Create<PreOrder>();

            var emptyOrder = Order.Empty(name);
            var orderFromPreOrder = Order.FromPreOrder(preOrder);

            emptyOrder.Id.Should().NotBeEmpty();
            orderFromPreOrder.Id.Should().NotBeEmpty();
        }

        [Fact]
        public void new_order_status_should_be_purchsed()
        {
            var order = Order.Empty("TestOrder");

            order.Status.ShouldBeEquivalentTo(Status.Purchased);
        }

        [Fact]
        public void new_order_crated_from_preorder_should_be_mapped_properly()
        {
            var preOrder = _fixture.Create<PreOrder>();

            var order = Order.FromPreOrder(preOrder);

            order.Should().NotBeNull();
            order.Name.ShouldAllBeEquivalentTo(preOrder.Name);
            order.Items.Count().ShouldBeEquivalentTo(preOrder.Items.Count());
        }
    }
}
