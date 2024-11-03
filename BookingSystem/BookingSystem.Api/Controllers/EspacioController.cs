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
    public class EspaciosController : ControllerBase
    {
        private readonly BookingSystemContext _context;

        public EspaciosController(BookingSystemContext context)
        {
            _context = context;
        }

        // GET: api/espacios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Espacio>>> ObtenerEspacios()
        {
            return await _context.Espacios.ToListAsync();
        }

        // GET: api/espacios/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Espacio>> ObtenerEspacio(int id)
        {
            var espacio = await _context.Espacios.FindAsync(id);

            if (espacio == null)
            {
                return NotFound();
            }

            return espacio;
        }

        // POST: api/espacios/crear
        [HttpPost("crear")]
        public async Task<ActionResult<Espacio>> CrearEspacio(Espacio espacio)
        {
            _context.Espacios.Add(espacio);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObtenerEspacio), new { id = espacio.Id }, espacio);
        }

        // PUT: api/espacios/editar/{id}
        [HttpPut("editar/{id}")]
        public async Task<IActionResult> EditarEspacio(int id, Espacio espacio)
        {
            if (id != espacio.Id)
            {
                return BadRequest();
            }

            _context.Entry(espacio).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/espacios/eliminar/{id}
        [HttpDelete("eliminar/{id}")]
        public async Task<IActionResult> EliminarEspacio(int id)
        {
            var espacio = await _context.Espacios.FindAsync(id);
            if (espacio == null)
            {
                return NotFound();
            }

            _context.Espacios.Remove(espacio);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
