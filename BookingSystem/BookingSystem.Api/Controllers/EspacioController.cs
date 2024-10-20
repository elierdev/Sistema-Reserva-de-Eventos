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
    public class EspaciosController : ControllerBase
    {
        private readonly BookingSystemContext _context;

        public EspaciosController(BookingSystemContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Espacio>>> GetEspacios()
        {
            return await _context.Espacios.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Espacio>> GetEspacio(int id)
        {
            var espacio = await _context.Espacios.FindAsync(id);

            if (espacio == null)
            {
                return NotFound();
            }

            return espacio;
        }

        [HttpPost]
        public async Task<ActionResult<Espacio>> PostEspacio(Espacio espacio)
        {
            _context.Espacios.Add(espacio);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEspacio), new { id = espacio.Id }, espacio);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEspacio(int id, Espacio espacio)
        {
            if (id != espacio.Id)
            {
                return BadRequest();
            }

            _context.Entry(espacio).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEspacio(int id)
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
