using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProgettoSettimanaleApi.Model.Entity
{
    public class Evento
    {

        [Key]
        public Guid EventoId { get; set; }

        [Required]
        public string Titolo { get; set; } = string.Empty;

        [Required]
        public DateTime Data { get; set; }

        [Required]
        public string Luogo { get; set; } = string.Empty;

        [Required]
        public Guid ArtistaId { get; set; }

        [ForeignKey(nameof(ArtistaId))]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Artista? Artista { get; set; }

        public ICollection<Biglietto>? Biglietti { get; set; }


    }
}
