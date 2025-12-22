namespace ProgettoSettimanaleApi.Model.DTOs.Responses
{
    public class BigliettoResponse
    {

        public Guid BigliettoId { get; set; }
        public Guid EventoId { get; set; }

        public string UserId { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public EventoDto? Evento { get; set; }

        public ArtistDto? Artista { get; set; }


    }
}
