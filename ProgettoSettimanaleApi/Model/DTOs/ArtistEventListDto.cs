using ProgettoSettimanaleApi.Model.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProgettoSettimanaleApi.Model.DTOs
{
    public class ArtistEventListDto
    {

        public Guid ArtistId { get; set; }


        public string Nome { get; set; } = string.Empty;


        public string Genere { get; set; } = string.Empty;


        public string Biografia { get; set; } = string.Empty;


        public List<EventListDto>? Eventi { get; set; }

    }
}
