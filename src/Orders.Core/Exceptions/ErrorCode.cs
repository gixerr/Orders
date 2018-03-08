using System.IO;

namespace Orders.Core.Exceptions
{
    public class ErrorCode
    {
        //TODO change to readonly props
        public const string empty_order_name = nameof(empty_order_name);
        public const string empty_category_name = nameof(empty_category_name);
        public const string empty_item_name = nameof(empty_item_name);
        public const string order_not_found = nameof(order_not_found);
        public const string category_not_found = nameof(category_not_found);
        public const string item_not_found = nameof(item_not_found);
        public static string user_not_found => nameof(user_not_found);
        public const string order_already_exists = nameof(order_already_exists);
        public const string category_already_exists = nameof(category_already_exists);
        public const string item_already_exists = nameof(item_already_exists);
        public static string user_already_exists => nameof(user_already_exists);
        public const string invalid_name = nameof(invalid_name);
        public const string invalid_price = nameof(invalid_price);
        public static string invalid_email => nameof(invalid_email);
        public static string invalid_password => nameof(invalid_password);
        public static string invalid_credentials => nameof(invalid_credentials);

    }

}