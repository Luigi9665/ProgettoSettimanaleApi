using Microsoft.EntityFrameworkCore;
using ProgettoSettimanaleApi.Model.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProgettoSettimanaleApi.Model.DTOs
{
    public class BigliettoDto
    {


        [Required]
        public Guid EventoId { get; set; }




    }
}
