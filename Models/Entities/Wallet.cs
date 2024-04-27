namespace WalletApi.Models.Entities
{
    public class Wallet
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Currency { get; set; } = "";
        public decimal Balance { get; set; } = 0;
        public string AppUserId { get; set; } = "";
        public AppUser AppUser { get; set; } 
    }
}
