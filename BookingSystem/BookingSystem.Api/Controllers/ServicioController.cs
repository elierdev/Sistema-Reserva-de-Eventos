using BookingSystem.Domain.Entities;
using BookingSystem.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiciosController : ControllerBase
    {
        private readonly BookingSystemContext _context;

        public ServiciosController(BookingSystemContext context)
        {
            _context = context;
        }

        // GET: api/servicios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Servicio>>> ObtenerServicios()
        {
            return await _context.Servicios.ToListAsync();
        }

        // GET: api/servicios/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Servicio>> ObtenerServicio(int id)
        {
            var servicio = await _context.Servicios.FindAsync(id);

            if (servicio == null)
            {
                return NotFound();
            }

            return servicio;
        }

        // POST: api/servicios/crear
        [HttpPost("crear")]
        public async Task<ActionResult<Servicio>> CrearServicio(Servicio servicio)
        {
            _context.Servicios.Add(servicio);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObtenerServicio), new { id = servicio.Id }, servicio);
        }

        // PUT: api/servicios/editar/{id}
        [HttpPut("editar/{id}")]
        public async Task<IActionResult> EditarServicio(int id, Servicio servicio)
        {
            if (id != servicio.Id)
            {
                return BadRequest();
            }

            _context.Entry(servicio).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/servicios/eliminar/{id}
        [HttpDelete("eliminar/{id}")]
        public async Task<IActionResult> EliminarServicio(int id)
        {
            var servicio = await _context.Servicios.FindAsync(id);
            if (servicio == null)
            {
                return NotFound();
            }

            _context.Servicios.Remove(servicio);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
