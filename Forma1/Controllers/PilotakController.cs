using Forma1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Digests;
using System.Reflection;

namespace Forma1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PilotakController : ControllerBase
    {
        [HttpGet]

        public IActionResult GetPilotak()
        {
            using (var cx = new Forma1Context())
            {
                try
                {
                    var pilotak = cx.Pilotaks.ToList();
                    return Ok(pilotak);
                }
                catch (Exception ex)
                {
                    return StatusCode(408, ex.Message);
                    return StatusCode(StatusCodes.Status408RequestTimeout, ex.Message);
                }
            }
        }
    }
}
