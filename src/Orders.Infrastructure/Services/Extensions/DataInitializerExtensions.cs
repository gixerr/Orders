using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Orders.Core.Domain;
using Orders.Infrastructure.Services.Interfaces;

namespace Orders.Infrastructure.Services.Extensions
{
    public static class DataInitializerExtensions
    {
        public static async Task InitializeOrdersAsync(this IOrderService orderService)
        {
            for (int i = 0; i <= 10; i++)
            {
                await orderService.AddEmptyAsync($"TestOrder-{i+1}");
            }
        }

        public static async Task InitializeCategoriesAsync(this ICategoryService categoryService)
        {
            for (int i = 0; i <= 10; i++)
            {
                await categoryService.AddAsync($"TestCategory-{i+1}");
            }
        }

        public static async Task InitializeItemsAsync(this IItemService itemService)
        {
            for (int i = 0; i <= 10; i++)
            {
                if (i < 5)
                {
                    await itemService.AddAsync($"TestItem-{i + 1}", i + 46, $"TestCategory-{i+1}");
                }
                else
                {
                    await itemService.AddAsync($"TestItem-{i - 4}", i + 93, $"TestCategory-{i+1}");
                }
            }
        }

        public static async Task InitializeUsersAsync(this IUserService userService)
        {
            for (int i = 0; i < 10; i++)
            {
                var user = new User($"TestUser-{i+1}", $"TestUser-{i+1}@email.com");
                var passsword = $"secret{i+1}";
                await userService.RegisterAsync(user.Name, user.Email, passsword, Role.User);
            }
        }
    }
}