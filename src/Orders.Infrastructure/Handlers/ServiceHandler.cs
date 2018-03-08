using Orders.Infrastructure.Services.Interfaces;

namespace Orders.Infrastructure.Handlers
{
    public abstract class ServiceHandler
    {
        protected readonly IOrderService _orderService;
        protected readonly ICategoryService _categoryService;
        protected readonly IItemService _itemService;
        protected readonly IUserService _userService;

        protected ServiceHandler(IOrderService orderService)
        {
            
            _orderService = orderService;
        }

        protected ServiceHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        protected ServiceHandler(IItemService itemService)
        {
            _itemService = itemService;
        }

        protected ServiceHandler(IUserService userService)
        {
            _userService = userService;
        }

    }
}