using System.ComponentModel.DataAnnotations;

namespace ProgettoSettimanaleApi.Model.DTOs
{
    public class EventForArtistResponse
    {
        public Guid EventoId { get; set; }
        public string Titolo { get; set; } = string.Empty;

        public DateTime Data { get; set; }

        public string Luogo { get; set; } = string.Empty;

    }
}
