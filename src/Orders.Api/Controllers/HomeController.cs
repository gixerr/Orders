using Microsoft.AspNetCore.Mvc;

namespace Orders.Api.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        => Content("Orders API.");        
    }
}