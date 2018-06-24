using System;
using AutoFixture;
using FluentAssertions;
using Orders.Core.Domain;
using Orders.Core.Exceptions;
using Xunit;

namespace Orders.Tests.Domain
{
    public class ItemTests
    {

        private readonly Fixture _fixture;
        private string name;
        private Category category;
        private decimal price;

        public ItemTests()
        {
            _fixture = new Fixture();
            name = _fixture.Create<string>();
            category = _fixture.Create<Category>();
            price = _fixture.Create<decimal>();
        }

        [Fact]
        public void providing_empty_name_should_throw_an_exception()
        {
            var name = string.Empty;

            Action createNewItem = () => new Item(name, price, category);

            var expectedException = createNewItem.ShouldThrow<OrdersException>();
            expectedException.And.ErrorCode.ShouldBeEquivalentTo(ErrorCode.invalid_name);
            expectedException.And.Message.ShouldBeEquivalentTo("Name cannot be empty.");
        }

        [Fact]
        public void passing_null_as_name_should_throw_an_exception()
        {
            string name = null;

            Action createNewItem = () => new Item(name, price, category);

            var expectedException = createNewItem.ShouldThrow<OrdersException>();
            expectedException.And.ErrorCode.ShouldBeEquivalentTo(ErrorCode.invalid_name);
            expectedException.And.Message.ShouldBeEquivalentTo("Name cannot be empty.");
        }

        [Fact]
        public void passing_null_as_category_should_throw_an_exception()
        {
            Action createNewItem = () => new Item(name, price, (Category)null);

            var expectedException = createNewItem.ShouldThrow<OrdersException>();
            expectedException.And.ErrorCode.ShouldBeEquivalentTo(ErrorCode.category_not_found);
            expectedException.And.Message.ShouldBeEquivalentTo("Category cannot be empty.");
        }

        [Fact]
        public void passing_value_even_zero_as_price_should_throw_an_exception()
        {
            price = 0;

            Action createNewItem = () => new Item(name, price, category);

            var expectedException = createNewItem.ShouldThrow<OrdersException>();
            expectedException.And.ErrorCode.ShouldBeEquivalentTo(ErrorCode.invalid_price);
            expectedException.And.Message.ShouldBeEquivalentTo($"Given price '{price}' is invalid. Price must be greater then 0.");
        }

        [Fact]
        public void passing_value_lower_than_zero_as_price_should_throw_an_exception()
        {
            price = -15;

            Action createNewItem = () => new Item(name, price, category);

            var expectedException = createNewItem.ShouldThrow<OrdersException>();
            expectedException.And.ErrorCode.ShouldBeEquivalentTo(ErrorCode.invalid_price);
            expectedException.And.Message.ShouldBeEquivalentTo($"Given price '{price}' is invalid. Price must be greater then 0.");
        }

        [Fact]
        public void new_item_id_should_not_be_empty()
        {
            var item = _fixture.Create<Item>();

            item.Id.Should().NotBeEmpty();
        }
    }
}
