using Orders.Infrastructure.Services.Interfaces;

namespace Orders.Infrastructure.Commands.Handlers
{
    public abstract class ServiceHandler
    {
        protected readonly IOrderService _orderService;
        protected readonly ICategoryService _categoryService;
        protected readonly IItemService _itemservice;

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
            _itemservice = itemService;
        } 
    }
}