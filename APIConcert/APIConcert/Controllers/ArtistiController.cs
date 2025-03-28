using APIConcert.Data;
using APIConcert.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIConcert.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArtistiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ArtistiController(AppDbContext context)
        {
            _context = context;
        }

        // api/artisti
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var artisti = await _context.Artisti.ToListAsync();
            return Ok(artisti);
        }

        // api/artisti/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var artista = await _context.Artisti.FindAsync(id);
            if (artista == null) return NotFound();
            return Ok(artista);
        }

        // api/artisti
        [HttpPost]
        [Authorize(Roles = "Amministratore")]
        public async Task<IActionResult> Create(Artista artista)
        {
            _context.Artisti.Add(artista);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = artista.ArtistaId }, artista);
        }

        // api/artisti/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Amministratore")]
        public async Task<IActionResult> Update(int id, Artista artista)
        {
            if (id != artista.ArtistaId)
                return BadRequest("ID non corrispondente");

            _context.Entry(artista).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // api/artisti/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Amministratore")]
        public async Task<IActionResult> Delete(int id)
        {
            var artista = await _context.Artisti.FindAsync(id);
            if (artista == null) return NotFound();

            _context.Artisti.Remove(artista);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
