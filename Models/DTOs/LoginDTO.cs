using System.ComponentModel.DataAnnotations;

namespace WalletApi.Models.DTOs
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress(ErrorMessage = "Enter a valid email address")]
        public string Email { get; set; } = "";

        [Required]
        public string Password { get; set; } = "";
    }
}
