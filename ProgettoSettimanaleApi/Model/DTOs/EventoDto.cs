using Microsoft.EntityFrameworkCore;
using ProgettoSettimanaleApi.Model.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProgettoSettimanaleApi.Model.DTOs
{
    public class EventoDto
    {



        [Required]
        public string Titolo { get; set; } = string.Empty;

        [Required]
        public DateTime Data { get; set; }

        [Required]
        public string Luogo { get; set; } = string.Empty;

        [Required]
        public Guid ArtistaId { get; set; }



    }
}
