using System.ComponentModel.DataAnnotations;

namespace ProgettoSettimanaleApi.Model.DTOs.Requests
{
    public sealed class LoginRequest
    {

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

    }
}
