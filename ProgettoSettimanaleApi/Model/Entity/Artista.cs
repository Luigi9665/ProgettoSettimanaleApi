using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProgettoSettimanaleApi.Model.Entity
{
    public class Artista
    {

        [Key]
        public Guid ArtistId { get; set; }

        [Required]
        public string Nome { get; set; } = string.Empty;

        [Required]
        public string Genere { get; set; } = string.Empty;


        public string Biografia { get; set; } = string.Empty;

        [InverseProperty(nameof(Evento.Artista))]
        public ICollection<Evento>? Eventi { get; set; }

    }
}
