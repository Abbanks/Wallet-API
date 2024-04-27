using System.ComponentModel.DataAnnotations;

namespace WalletApi.Models.DTOs
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "User name is required")]
        [Display(Name = "User Name")]
        public string UserName { get; set; } = "";
       
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter a valid email address")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Main currency is required")]

        [RegularExpression(@"^\b[A-Z]{3}\b$", ErrorMessage = "Currency must be a 3-letter currency code")]
        public string MainCurrency { get; set; } = "";

        [Required(ErrorMessage = "User Type is required")]
        [RegularExpression(@"(?i)^(\b(Elite|Noob)\b)$", ErrorMessage = "User Type must be either a Noob or an Elite")]
        public string UserType { get; set; } = "";  
 
        [Required(ErrorMessage = "Password must be at least 8 characters long and contain at least one letter and one digit")]
        public string Password { get; set; } = "";
    }
}
