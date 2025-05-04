using Microsoft.AspNetCore.Mvc;
using MyApi.Data;

namespace MyApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class WalletsController : ControllerBase
    {
        private readonly DataContext _context;

        public WalletsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
