using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WalletApi.Models.Entities
{
    public class AppUser : IdentityUser
    {
        public string MainCurrency { get; set; } = "";

        public IList<Wallet> Wallets { get; set; } = new List<Wallet>();

        public decimal TotalBalance { get; set; }   
    }
}
 