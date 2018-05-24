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
            var orderDtos = await _orderService.GetAllAsync();

            return Ok(orderDtos);
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var orderDto = await _orderService.GetAsync(id);

            return Ok(orderDto);
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> Get(string name)
        {
            var orderDto = await _orderService.GetAsync(name);

            return Ok(orderDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddEmptyOrder command)
        {
            await CommandDispatcher.DispatchAsync(command);

            return Created($"orders/{command.Name}", null);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateOrder command)
        {
            await CommandDispatcher.DispatchAsync(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(RemoveOrder command)
        {
            await CommandDispatcher.DispatchAsync(command);

            return NoContent();
        }
    }
}