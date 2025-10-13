using AutoSellCourses.AppData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoSellCourses.Controllers
{
    [Route("api/client")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly AutoSellContext _context;


        public ClientController(AutoSellContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetClient()
        {
            var clients = _context.Clients
                .Select(client => new
                {
                    id = client.ClientId,
                    name = client.ClientName
                }
                ).ToList();
            if(clients is null || clients.Count == 0)
            {
                return NotFound();
            }
            return Ok(clients);

        }


    }
}
