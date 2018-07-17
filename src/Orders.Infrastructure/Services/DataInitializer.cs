using System.Threading.Tasks;
using Orders.Infrastructure.Services.Extensions;
using Orders.Infrastructure.Services.Interfaces;
using Orders.Infrastructure.Settings;

namespace Orders.Infrastructure.Services
{
    public class DataInitializer : IDataInitializer
    {
        private readonly IOrderService _orderService;
        private readonly ICategoryService _categoryService;
        private readonly IItemService _itemService;
        private readonly IUserService _userService;

        public DataInitializer(IOrderService orderService, ICategoryService categoryService, IItemService itemService,
            IUserService userService)
        {
            _orderService = orderService;
            _categoryService = categoryService;
            _itemService = itemService;
            _userService = userService;
        }
        public async Task InitializeData(DataSettings dataSettings)
        {
            if(dataSettings.InitializeOrders)
            {
                await _orderService.InitializeOrdersAsync();
            }

            if(dataSettings.InitializeCategoriesAndItems)
            {
                await _categoryService.InitializeCategoriesAsync();
                await _itemService.InitializeItemsAsync();
            }

            if(dataSettings.InitializeUsers)
            {
                await _userService.InitializeUsersAsync();
            }
            
        }
    }
}