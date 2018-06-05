using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Orders.Infrastructure.Commands.Interfaces;
using Orders.Infrastructure.Commands.OrderItems;
using Orders.Infrastructure.Services.Interfaces;

namespace Orders.Api.Controllers
{
    public class OrderItemsController : BaseController
    {
        private readonly IOrderItemService _orderItemService;

        public OrderItemsController(ICommandDispatcher commandDispatcher, IOrderItemService orderItemService)
            : base(commandDispatcher)
        {
            _orderItemService = orderItemService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddOrderItem command)
        {
            await CommandDispatcher.DispatchAsync(command);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] RemoveOrderItem command)
        {
            await CommandDispatcher.DispatchAsync(command);
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Clear(Guid id)
        {
            await _orderItemService.ClearAsync(id);

            return NoContent();
        }
    }
}