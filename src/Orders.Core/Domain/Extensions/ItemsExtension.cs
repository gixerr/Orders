using System.Collections.Generic;
using System.Linq;

namespace Orders.Core.Domain.Extensions
{
    public static class ItemsExtension
    {
        public static IEnumerable<Item> GetItems(this IEnumerable<Item> items, string name)
            => items.Where(i => i.Name.ToLowerInvariant() == name.ToLowerInvariant());

        public static Item GetItem(this IEnumerable<Item> items, string itemName, string categoryName)
            => items.SingleOrDefault(i => 
                i.Name.ToLowerInvariant() == itemName.ToLowerInvariant() 
                && i.Category.Name.ToLowerInvariant() == categoryName.ToLowerInvariant());
    }
}