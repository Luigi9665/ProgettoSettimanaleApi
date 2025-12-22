namespace ProgettoSettimanaleApi.Model.DTOs.Responses
{
    public class ArtistaResponse
    {
        public Guid ArtistaId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Genere { get; set; } = string.Empty;
        public string Biografia { get; set; } = string.Empty;
        public List<EventForArtistResponse> Eventi { get; set; } = new();
        public List<BigliettiEventoDto> Biglietti { get; set; } = new();
    }
}
