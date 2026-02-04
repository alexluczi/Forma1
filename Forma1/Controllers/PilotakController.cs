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

        [HttpGet("pnev")]
        public async Task<IActionResult> GetPilotaByNev(string pnev)
        {
            try
            {
                using (var cx = new Forma1Context())
                {
                    var pilota = await cx.Pilotaks
                        .Where(p => p.Pnev == pnev)
                        .Include(p => p.CsapatNavigation)
                        .Include(p => p.Eredmenyeks)
                        .Select(p => new
                        {
                            Pazon = p.Pazon,
                            Pnev = p.Pnev,
                            Szev = p.Szev,
                            Csapat = p.CsapatNavigation == null ? null : new
                            {
                                Csazon = p.CsapatNavigation.Csazon,
                                Csnev = p.CsapatNavigation.Csnev
                            },
                            Eredmenyeks = p.Eredmenyeks.Select(e => new
                            {
                                Nagydij = e.Nagydij,
                                Startpoz = e.Startpoz,
                                Celpoz = e.Celpoz
                            }).ToList()
                        })
                        .FirstOrDefaultAsync();

                    if (pilota == null)
                        return NotFound();

                    return Ok(pilota);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
