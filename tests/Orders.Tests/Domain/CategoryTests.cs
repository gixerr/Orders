using System;
using AutoFixture;
using FluentAssertions;
using Orders.Core.Domain;
using Orders.Core.Exceptions;
using Xunit;

namespace Orders.Tests.Domain
{
    public class CategoryTests
    {
        private readonly Fixture _fixture;

        public CategoryTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void providing_empty_name_should_throw_an_exception()
        {
            var name = string.Empty;

            Action createNewCategory = () => new Category(name);

            var expectedException = createNewCategory.ShouldThrow<OrdersException>();
            expectedException.And.ErrorCode.ShouldBeEquivalentTo(ErrorCode.invalid_name);
            expectedException.And.Message.ShouldBeEquivalentTo("Name cannot be empty.");
        }

        [Fact]
        public void passing_null_as_name_should_throw_an_exception()
        {
            string name = null;

            Action createNewCategory = () => new Category(name);

            var expectedException = createNewCategory.ShouldThrow<OrdersException>();
            expectedException.And.ErrorCode.ShouldBeEquivalentTo(ErrorCode.invalid_name);
            expectedException.And.Message.ShouldBeEquivalentTo("Name cannot be empty.");
        }

        [Fact]
        public void new_category_id_should_not_be_empty()
        {
            var category = _fixture.Create<Category>();

            category.Id.Should().NotBeEmpty();
        }
    }
}
