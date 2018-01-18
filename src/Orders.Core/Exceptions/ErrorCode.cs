namespace Orders.Core.Exceptions
{
    public class ErrorCode
    {
        public const string empty_order_name = nameof(empty_order_name);
        public const string empty_category_name = nameof(empty_category_name);
        public const string empty_item_name = nameof(empty_item_name);
        public const string item_not_found = nameof(item_not_found);
    }
}