using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProgettoSettimanaleApi.Model.Entity
{
    public class Biglietto
    {
        [Key]
        public Guid BigliettoId { get; set; }

        [Required]
        public Guid EventoId { get; set; }

        [ForeignKey(nameof(EventoId))]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Evento Evento { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = string.Empty;

        [ForeignKey(nameof(UserId))]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public ApplicationUser? User { get; set; }

    }
}
