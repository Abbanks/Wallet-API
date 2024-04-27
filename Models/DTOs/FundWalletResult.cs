namespace WalletApi.Models.DTOs
{
    public class FundWalletResult
    {
        public string UserId { get; set; } = "";
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = "";
    }
}
