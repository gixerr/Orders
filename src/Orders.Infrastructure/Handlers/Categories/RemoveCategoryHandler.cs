using System.Threading.Tasks;
using Orders.Infrastructure.Commands.Categories;
using Orders.Infrastructure.Commands.Interfaces;
using Orders.Infrastructure.Services.Interfaces;

namespace Orders.Infrastructure.Handlers.Categories
{
    public class RemoveCategoryHandler : ServiceHandler, ICommandHandler<RemoveCategory>
    {
        public RemoveCategoryHandler(ICategoryService categoryService) : base(categoryService) { }
        
        public async Task HandleAsync(RemoveCategory command)
            => await _categoryService.RemoveAsync(command.Id);
    }
}