using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Orders.Infrastructure.Commands.Categories;
using Orders.Infrastructure.Commands.Interfaces;
using Orders.Infrastructure.Services.Interfaces;

namespace Orders.Api.Controllers
{
    public class CategoriesController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICommandDispatcher commandDispatcher, ICategoryService categoryService) : base(commandDispatcher)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categories = await _categoryService.GetAllAsync();

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var category = await _categoryService.GetAsync(id);

            return Ok(category);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> Get(string name)
        {
            var category = await _categoryService.GetAsync(name);

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddCategory command)
        {
            await CommandDispatcher.DispatchAsync(command);

            return Created($"categories/{command.Name}", null);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(RemoveCategory command)
        {
            await CommandDispatcher.DispatchAsync(command);

            return NoContent();
        }
    }
}
