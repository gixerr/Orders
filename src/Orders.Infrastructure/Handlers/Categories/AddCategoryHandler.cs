using System.Threading.Tasks;
using Orders.Infrastructure.Commands.Categories;
using Orders.Infrastructure.Commands.Interfaces;
using Orders.Infrastructure.Services.Interfaces;

namespace Orders.Infrastructure.Handlers.Categories
{
    public class AddCategoryHandler : ServiceHandler, ICommandHandler<AddCategory>
    {
        public AddCategoryHandler(ICategoryService categoryService) : base(categoryService) { }

        public async Task HandleAsync(AddCategory command)
            => await _categoryService.AddAsync(command.Name);
    }
}