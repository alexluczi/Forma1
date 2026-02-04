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
        public async Task<IActionResult> GetPilota(int pazon)
        {
            try
            {
                using (var cx = new Forma1Context())
                {
                    var pilota = await cx.Pilotaks
                        .Where(p => p.Pazon == pazon)
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

        [HttpDelete("{pazon}")]
        public IActionResult DeletePilota(int pazon)
        {
            try
            {
                using (var cx = new Forma1Context())
                {
                    var pilota = cx.Pilotaks
                        .Include(p => p.Eredmenyeks)
                        .FirstOrDefault(p => p.Pazon == pazon);

                    if (pilota == null)
                        return NotFound("A pilóta nem található!");

                    cx.Eredmenyeks.RemoveRange(pilota.Eredmenyeks);

                    cx.Pilotaks.Remove(pilota);
                    cx.SaveChanges();
                    return Ok("A pilóta sikeresen törölve!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post(Pilotak pilota)
        {
            try
            {
                using (var cx = new Forma1Context())
                {
                    cx.Pilotaks.Add(pilota);
                    cx.SaveChanges();
                    return Ok("Pilóta felvétele sikeresen megtörtént!");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(410, ex.Message);
            }
        }
    }
}
