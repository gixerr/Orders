using Orders.Infrastructure.Services.Interfaces;

namespace Orders.Infrastructure.Handlers
{
    public abstract class ServiceHandler
    {
        protected readonly IOrderService _orderService;
        protected readonly ICategoryService _categoryService;
        protected readonly IItemService _itemService;
        protected readonly IUserService _userService;
        protected readonly IPreOrderService _preOrderService;
        protected readonly IPreOrderItemService _preOrderItemService;
        protected readonly IOrderItemService _orderItemService;

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

        protected ServiceHandler(IPreOrderService preOrderService)
        {
            _preOrderService = preOrderService;
        }

        protected ServiceHandler(IPreOrderItemService preOrderItemService)
        {
            _preOrderItemService = preOrderItemService;
        }
        
        protected ServiceHandler(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }
        
        

    }
}