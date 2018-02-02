using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Orders.Infrastructure.Commands.Interfaces;
using Orders.Infrastructure.Commands.Items;
using Orders.Infrastructure.Services.Interfaces;

namespace Orders.Api.Controllers
{
    [Route("items")]
    public class ItemsController : BaseController
    {
        private readonly IItemService _itemService;

        public ItemsController(ICommandDispatcher commandDispatcher, IItemService itemService) : base(commandDispatcher)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var items = await _itemService.GetAllAsync();

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var item = await _itemService.GetAsync(id);

            return Ok(item);
        }

        [HttpGet("{name")]
        public async Task<IActionResult> Get(string name)
        {
            var items = await _itemService.GetAsync(name);

            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddItem command)
        {
            await CommandDispatcher.DispatchAsync(command);

            return Created($"items/{command.itemName}", null);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(RemoveItem command)
        {
            await CommandDispatcher.DispatchAsync(command);

            return NoContent();
        }
    }
}