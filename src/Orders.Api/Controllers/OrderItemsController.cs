using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Orders.Infrastructure.Commands.Interfaces;
using Orders.Infrastructure.Commands.OrderItems;

namespace Orders.Api.Controllers
{
    public class OrderItemsController : BaseController
    {
        public OrderItemsController(ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
            
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddOrderItem command)
        {
            await CommandDispatcher.DispatchAsync(command);

            return Ok();
        }

    }
}