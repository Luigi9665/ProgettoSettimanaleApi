using System.ComponentModel.DataAnnotations;

namespace ProgettoSettimanaleApi.Model.DTOs
{
    public class ArtistDto
    {

        [Required]
        public string Nome { get; set; } = string.Empty;

        [Required]
        public string Genere { get; set; } = string.Empty;


        public string Biografia { get; set; } = string.Empty;

    }
}
