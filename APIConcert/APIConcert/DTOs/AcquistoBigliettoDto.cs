using System.ComponentModel.DataAnnotations;

namespace APIConcert.DTOs
{
    public class AcquistoBigliettoDto
    {
        [Required]
        public int EventoId { get; set; }

        [Range(1, 10, ErrorMessage = "You can buy between 1 and 10 tickets at a time")]
        public int Quantita { get; set; } = 1;
    }
}
