using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Orders.Infrastructure.Commands.Interfaces;
using Orders.Infrastructure.Commands.Orders;
using Orders.Infrastructure.Services.Interfaces;

namespace Orders.Api.Controllers
{
    public class OrdersController : BaseController
    {
        private readonly IOrderService _orderService;

        public OrdersController(ICommandDispatcher commandDispatcher, IOrderService orderService) : base(commandDispatcher)
        {
            _orderService = orderService;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var orders = await _orderService.GetAllAsync();

            return Ok(orders);
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var order = await _orderService.GetAsync(id);

            return Ok(order);
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> Get(string name)
        {
            var order = await _orderService.GetAsync(name);

            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AddOrder command)
        {
            await CommandDispatcher.DispatchAsync(command);

            return Created($"orders/{command.Name}", null);
        }

        [HttpDelete("{id}")] 
        public async Task<IActionResult> Delete(RemoveOrder command)
        {
            await CommandDispatcher.DispatchAsync(command);

            return NoContent();
        }
    }
}