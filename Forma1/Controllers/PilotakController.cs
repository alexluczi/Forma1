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

        [HttpGet("{pazon}")]

        public IActionResult GetPilota(int pazon)
        {
            try
            {
                using (var cx = new Forma1Context())
                {
                    var pilota = cx.Pilotaks.FirstOrDefault(p => p.Pazon == pazon);
                    return Ok(pilota);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(401, ex.InnerException.Message);
            }
        }
    }
}
