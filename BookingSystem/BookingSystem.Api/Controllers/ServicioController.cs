using BookingSystem.Domain.Entities;
using BookingSystem.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookingSystem.Domain;
using BookingSystem.Domain.Entities;

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Servicio>>> GetServicios()
        {
            return await _context.Servicios.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Servicio>> GetServicio(int id)
        {
            var servicio = await _context.Servicios.FindAsync(id);

            if (servicio == null)
            {
                return NotFound();
            }

            return servicio;
        }

        [HttpPost]
        public async Task<ActionResult<Servicio>> PostServicio(Servicio servicio)
        {
            _context.Servicios.Add(servicio);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetServicio), new { id = servicio.Id }, servicio);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutServicio(int id, Servicio servicio)
        {
            if (id != servicio.Id)
            {
                return BadRequest();
            }

            _context.Entry(servicio).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServicio(int id)
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
