using System.ComponentModel.DataAnnotations;

namespace ProgettoSettimanaleApi.Model.DTOs.Requests
{
    public sealed class RegisterRequest
    {

        [Required]
        public string FirsName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [EmailAddress]
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        [Compare(nameof(Email))]
        public string ConfirmEmail { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = string.Empty;
        [Required]
        public string UserName { get; set; } = string.Empty;

    }
}
