using System;
using System.Collections.Generic;
using System.Linq;

namespace Orders.Core.Domain.Extensions
{
    public static class PreOrderItemsExtensions
    {
        public static PreOrderItem GetItem(this IEnumerable<PreOrderItem> items, Guid id)
            => items.SingleOrDefault(x => x.Id == id);
    }
}