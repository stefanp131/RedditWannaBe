using Microsoft.AspNetCore.Mvc;

namespace RedditWannaBe.API.Controllers
{
    [Route("api/test")]
    [ApiController]
    public class TestController: ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("test");
        }
    }
}
