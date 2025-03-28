using APIConcert.Data;
using APIConcert.DTOs;
using APIConcert.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace APIConcert.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BigliettiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BigliettiController(AppDbContext context)
        {
            _context = context;
        }

        // api/biglietti
        [HttpPost]
        [Authorize(Roles = "Utente")]
        public async Task<IActionResult> Acquista([FromBody] AcquistoBigliettoDto dto)
        {
            var evento = await _context.Eventi.FindAsync(dto.EventoId);
            if (evento == null)
                return NotFound("Evento non trovato");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            var biglietti = new List<Biglietto>();

            for (int i = 0; i < dto.Quantita; i++)
            {
                biglietti.Add(new Biglietto
                {
                    EventoId = dto.EventoId,
                    UserId = userId,
                    DataAcquisto = DateTime.UtcNow
                });
            }

            _context.Biglietti.AddRange(biglietti);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                messaggio = "Biglietti acquistati!",
                biglietti = biglietti.Select(b => new
                {
                    b.BigliettoId,
                    b.EventoId,
                    b.DataAcquisto
                })
            });
        }

        // api/biglietti/miei
        [HttpGet("miei")]
        [Authorize(Roles = "Utente")]
        public async Task<IActionResult> GetMieiBiglietti()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var mieiBiglietti = await _context.Biglietti
                .Where(b => b.UserId == userId)
                .Include(b => b.Evento)
                .ToListAsync();

            return Ok(mieiBiglietti);
        }

        // api/biglietti
        [HttpGet]
        [Authorize(Roles = "Amministratore")]
        public async Task<IActionResult> GetTutti()
        {
            var biglietti = await _context.Biglietti
                .Include(b => b.Evento)
                .Include(b => b.User)
                .ToListAsync();

            return Ok(biglietti);
        }
    }
}
