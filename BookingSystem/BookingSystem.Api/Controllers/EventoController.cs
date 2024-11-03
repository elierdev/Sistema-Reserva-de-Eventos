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
    public class EventosController : ControllerBase
    {
        private readonly BookingSystemContext _context;

        public EventosController(BookingSystemContext context)
        {
            _context = context;
        }

        // GET: api/eventos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Evento>>> ObtenerEventos()
        {
            return await _context.Eventos.ToListAsync();
        }

        // GET: api/eventos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Evento>> ObtenerEvento(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);

            if (evento == null)
            {
                return NotFound();
            }

            return evento;
        }

        // POST: api/eventos/crear
        [HttpPost("crear")]
        public async Task<ActionResult<Evento>> CrearEvento(Evento evento)
        {
            _context.Eventos.Add(evento);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObtenerEvento), new { id = evento.Id }, evento);
        }

        // PUT: api/eventos/editar/{id}
        [HttpPut("editar/{id}")]
        public async Task<IActionResult> EditarEvento(int id, Evento evento)
        {
            if (id != evento.Id)
            {
                return BadRequest();
            }

            _context.Entry(evento).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/eventos/eliminar/{id}
        [HttpDelete("eliminar/{id}")]
        public async Task<IActionResult> EliminarEvento(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);
            if (evento == null)
            {
                return NotFound();
            }

            _context.Eventos.Remove(evento);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
