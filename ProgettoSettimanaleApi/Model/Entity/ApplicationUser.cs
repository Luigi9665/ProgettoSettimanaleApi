using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProgettoSettimanaleApi.Model.Entity
{
    public class ApplicationUser : IdentityUser
    {

        public bool IsDeleted { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required(ErrorMessage = "Il nome è obbligatorio")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Il cognome è obbligatorio")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "La data è obbligatoria")]
        public DateOnly DateOfBirth { get; set; }

        [InverseProperty(nameof(Biglietto.User))]
        public ICollection<Biglietto>? Biglietti { get; set; }

    }
}
