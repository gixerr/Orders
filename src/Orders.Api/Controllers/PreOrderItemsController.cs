using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Orders.Infrastructure.Commands.Interfaces;
using Orders.Infrastructure.Commands.PreOrderItems;
using Orders.Infrastructure.Services.Interfaces;

namespace Orders.Api.Controllers
{
    public class PreOrderItemsController : BaseController
    {
        private readonly IPreOrderItemService _preOrderItemService;

        public PreOrderItemsController(ICommandDispatcher commandDispatcher, 
            IPreOrderItemService preOrderItemService) : base(commandDispatcher)
        {
            _preOrderItemService = preOrderItemService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Post([FromBody] AddItem command)
        {
            await CommandDispatcher.DispatchAsync(command);

            return Ok();
        }

        [HttpDelete("delete")]
        public IActionResult Remove([FromBody]RemoveItem command)
        {
            _preOrderItemService.Remove(command.ItemId, command.OrderName);

            return NoContent();
        }
    }
}