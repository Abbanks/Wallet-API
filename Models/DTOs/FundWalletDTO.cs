using System.ComponentModel.DataAnnotations;

namespace WalletApi.Models.DTOs
{
    public class FundWalletDTO
    {
        [RegularExpression(@"^\b[A-Z]{3}\b$", ErrorMessage = "Currency must be a 3-letter currency code")]
        public string Currency { get; set; } = ""; 
        public decimal Amount { get; set; }

 
    }
}
