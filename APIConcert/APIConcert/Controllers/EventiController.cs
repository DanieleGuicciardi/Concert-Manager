using APIConcert.Data;
using APIConcert.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIConcert.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EventiController(AppDbContext context)
        {
            _context = context;
        }

        // api/eventi
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var eventi = await _context.Eventi
                .Include(e => e.Artista)
                .ToListAsync();

            return Ok(eventi);
        }

        // api/eventi/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var evento = await _context.Eventi
                .Include(e => e.Artista)
                .FirstOrDefaultAsync(e => e.EventoId == id);

            if (evento == null)
                return NotFound();

            return Ok(evento);
        }

        // api/eventi
        [HttpPost]
        [Authorize(Roles = "Amministratore")]
        public async Task<IActionResult> Create(Evento evento)
        {
            var artista = await _context.Artisti.FindAsync(evento.ArtistaId);
            if (artista == null)
                return BadRequest("Artista non valido");

            _context.Eventi.Add(evento);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = evento.EventoId }, evento);
        }

        // api/eventi/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Amministratore")]
        public async Task<IActionResult> Update(int id, Evento evento)
        {
            if (id != evento.EventoId)
                return BadRequest("ID non corrispondente");

            _context.Entry(evento).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // api/eventi/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Amministratore")]
        public async Task<IActionResult> Delete(int id)
        {
            var evento = await _context.Eventi.FindAsync(id);
            if (evento == null)
                return NotFound();

            _context.Eventi.Remove(evento);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
