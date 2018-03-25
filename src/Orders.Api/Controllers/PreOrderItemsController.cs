using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Orders.Infrastructure.Commands.Interfaces;
using Orders.Infrastructure.Commands.PreOrderItems;

namespace Orders.Api.Controllers
{
    public class PreOrderItemsController : BaseController
    {
        public PreOrderItemsController(ICommandDispatcher commandDispatcher) : base(commandDispatcher) { }
        
        [HttpPost("add")]
        public async Task<IActionResult> Post([FromBody]AddItem command)
        {
            await CommandDispatcher.DispatchAsync(command);

            return Ok();
            
        }
    }
}