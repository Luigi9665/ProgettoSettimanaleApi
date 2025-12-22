namespace ProgettoSettimanaleApi.Model.DTOs
{
    public class EventListDto
    {
        public Guid EventoId { get; set; }
        public string Titolo { get; set; } = string.Empty;
        public DateTime Data { get; set; }
        public string Luogo { get; set; }

        public ArtistDto Artista { get; set; } = null!;
        public List<BigliettiEventoDto> Biglietti { get; set; } = new();
    }
}
