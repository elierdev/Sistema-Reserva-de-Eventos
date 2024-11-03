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
    public class ReservasController : ControllerBase
    {
        private readonly BookingSystemContext _context;

        public ReservasController(BookingSystemContext context)
        {
            _context = context;
        }

        // GET: api/reservas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reserva>>> ObtenerReservas()
        {
            return await _context.Reservas.ToListAsync();
        }

        // GET: api/reservas/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Reserva>> ObtenerReserva(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);

            if (reserva == null)
            {
                return NotFound();
            }

            return reserva;
        }

        // POST: api/reservas/crear
        [HttpPost("crear")]
        public async Task<ActionResult<Reserva>> CrearReserva(Reserva reserva)
        {
            _context.Reservas.Add(reserva);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObtenerReserva), new { id = reserva.Id }, reserva);
        }

        // PUT: api/reservas/editar/{id}
        [HttpPut("editar/{id}")]
        public async Task<IActionResult> EditarReserva(int id, Reserva reserva)
        {
            if (id != reserva.Id)
            {
                return BadRequest();
            }

            _context.Entry(reserva).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/reservas/eliminar/{id}
        [HttpDelete("eliminar/{id}")]
        public async Task<IActionResult> EliminarReserva(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }

            _context.Reservas.Remove(reserva);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
