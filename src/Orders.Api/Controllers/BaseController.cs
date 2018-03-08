using Microsoft.AspNetCore.Mvc;
using Orders.Infrastructure.Commands.Interfaces;

namespace Orders.Api.Controllers
{
    [Route("[controller]")]
    public class BaseController : Controller
    {
        protected readonly ICommandDispatcher CommandDispatcher;

        public BaseController(ICommandDispatcher commandDispatcher)
        {
            this.CommandDispatcher = commandDispatcher;
        }
    }
}
