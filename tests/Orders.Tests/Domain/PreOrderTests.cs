using System;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using Orders.Core.Domain;
using Orders.Core.Exceptions;
using Xunit;

namespace Orders.Tests.Domain
{
    public class PreOrderTests
    {
        private readonly Fixture _fixture;

        public PreOrderTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void providing_empty_name_should_throw_an_exception()
        {
            var name = string.Empty;

            Action createNewPreOrder = () => new PreOrder(name);

            var expectedException = createNewPreOrder.ShouldThrow<OrdersException>();
            expectedException.And.ErrorCode.ShouldBeEquivalentTo(ErrorCode.empty_order_name);
            expectedException.And.Message.ShouldBeEquivalentTo("Name cannot be empty.");
        }

        [Fact]
        public void passing_null_as_name_should_throw_an_exception()
        {
            string name = null;

            Action createNewPreOrder = () => new PreOrder(name);

            var expectedException = createNewPreOrder.ShouldThrow<OrdersException>();
            expectedException.And.ErrorCode.ShouldBeEquivalentTo(ErrorCode.empty_order_name);
            expectedException.And.Message.ShouldBeEquivalentTo("Name cannot be empty.");
        }

        [Fact]
        public void adding_not_contained_item_should_extend_items_list_and_should_not_increase_item_counter()
        {   
            var preOrder = _fixture.Create<PreOrder>();
            var preOrderItem = _fixture.Create<PreOrderItem>();
            
            preOrder.AddItem(preOrderItem);
            var addedItem = preOrder.Items.SingleOrDefault(x => x.Id == preOrderItem.Id);

            preOrder.Items.Count().ShouldBeEquivalentTo(1);
            addedItem.ShouldBeEquivalentTo(preOrderItem);
            addedItem.Counter.Value.ShouldBeEquivalentTo(1);
        }

        [Fact]
        public void adding_contained_item_should_not_extend_items_list_and_increase_item_counter()
        {
            var preOrder = _fixture.Create<PreOrder>();
            var preOrderItem = _fixture.Create<PreOrderItem>();

            preOrder.AddItem(preOrderItem);
            preOrder.AddItem(preOrderItem);

            var addedItem = preOrder.Items.SingleOrDefault(x => x.Id == preOrderItem.Id);

            preOrder.Items.Count().ShouldBeEquivalentTo(1);
            addedItem.Id.ShouldBeEquivalentTo(preOrderItem.Id);
            addedItem.Counter.Value.ShouldBeEquivalentTo(2);
        }

        [Fact]
        public void removing_not_contained_item_should_throw_an_exception()
        {
            var preOrder = _fixture.Create<PreOrder>();
            var preOrderItem = _fixture.Create<PreOrderItem>();

            Action removingNotContainedItem = () => preOrder.RemoveItem(preOrderItem.Id);

            var expectedException = removingNotContainedItem.ShouldThrow<OrdersException>();
            expectedException.And.ErrorCode.ShouldBeEquivalentTo(ErrorCode.item_not_found);
            expectedException.And.Message.ShouldBeEquivalentTo($"Item with given id '{preOrderItem.Id}' not found. Unable to remove nonexiting item.");
        }

        [Fact]
        public void removing_contained_item_where_counter_is_less_then_two_should_delete_item_from_list()
        {
            var preOrder = _fixture.Create<PreOrder>();
            var preOrderItem = _fixture.Create<PreOrderItem>();

            preOrder.AddItem(preOrderItem);
            preOrder.RemoveItem(preOrderItem.Id);

            preOrder.Items.SingleOrDefault(x => x.Id == preOrderItem.Id).Should().BeNull();
            preOrder.Items.Count().ShouldBeEquivalentTo(0);
        }

        [Fact]
        public void removing_contained_item_where_counter_is_greater_then_one_should_decrease_item_counter()
        {
            var preOrder = _fixture.Create<PreOrder>();
            var preOrderItem = _fixture.Create<PreOrderItem>();

            preOrder.AddItem(preOrderItem);
            preOrder.AddItem(preOrderItem);
            preOrder.RemoveItem(preOrderItem.Id);

            var deletedItem = preOrder.Items.SingleOrDefault(x => x.Id == preOrderItem.Id);

            deletedItem.Should().NotBeNull();
            deletedItem.Counter.Value.ShouldBeEquivalentTo(1);
            deletedItem.Id.ShouldBeEquivalentTo(preOrderItem.Id);
            preOrder.Items.Count().ShouldBeEquivalentTo(1);
        }
    }
}