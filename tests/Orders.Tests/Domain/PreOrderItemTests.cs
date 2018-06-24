using AutoFixture;
using FluentAssertions;
using Orders.Core.Domain;
using Xunit;

namespace Orders.Tests.Domain
{
    public class PreOrderItemTests
    {
        private readonly Fixture _fixrure;

        public PreOrderItemTests()
        {
            _fixrure = new Fixture();
        }

        [Fact]
        public void new_preOrderItem_counter_should_equals_one()
        {
            var preOrderItem = _fixrure.Create<PreOrderItem>();

            preOrderItem.Counter.Value.ShouldBeEquivalentTo(1);
        }
    }
}