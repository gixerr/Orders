using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Orders.Infrastructure.Commands.Interfaces;
using Orders.Infrastructure.Commands.PreOrders;
using Orders.Infrastructure.Services.Interfaces;

namespace Orders.Api.Controllers
{
    public class PreOrdersController : BaseController
    {
        private readonly IPreOrderService _preOrderService;

        public PreOrdersController(ICommandDispatcher commandDispatcher, IPreOrderService preOrderService) : base(commandDispatcher)
        {
            _preOrderService = preOrderService;
        }

        [HttpPost("create/{name}")]
        public async Task<IActionResult> Post(CreatePreOrder command)
        {
            await CommandDispatcher.DispatchAsync(command);

            return Ok();
        }

        [HttpPost("save/{name}")]
        public async Task<IActionResult> Post(SavePreOrder command)
        {
            await CommandDispatcher.DispatchAsync(command);

            return Created($"orders/{command.Name}", null);
        }

        [HttpPost]

        [HttpDelete("delete/{name}")]
        public IActionResult Delete(RemovePreOrder command)
        {
            //TODO create no async CommandDispatcher
            _preOrderService.Remove(command.Name);

            return NoContent();
        }


    }
}