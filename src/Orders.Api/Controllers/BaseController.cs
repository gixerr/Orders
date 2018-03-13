using System;
using Microsoft.AspNetCore.Mvc;
using Orders.Infrastructure.Commands.Interfaces;

namespace Orders.Api.Controllers
{
    [Route("[controller]")]
    public class BaseController : Controller
    {
        protected readonly ICommandDispatcher CommandDispatcher;

        protected Guid UserId => User?.Identity?.IsAuthenticated is true ?
            Guid.Parse(User.Identity.Name) : Guid.Empty;

        public BaseController(ICommandDispatcher commandDispatcher)
        {
            this.CommandDispatcher = commandDispatcher;
        }
    }
}
